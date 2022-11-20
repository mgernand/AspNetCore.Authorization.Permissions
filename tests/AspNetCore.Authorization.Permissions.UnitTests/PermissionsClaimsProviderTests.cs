namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using FluentAssertions;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class PermissionsClaimsProviderTests
	{
		private class TestClaimsProvider : IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
				});
			}
		}

		private class TestClaimsProviderWithDuplicatePermissions : IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
				});
			}
		}

		private class TestClaimsProviderWithWrongClaimType : IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(ClaimTypes.Role, "Invoices.Write"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
				});
			}
		}

		private class TestClaimsProviderWithNullResult : IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(null);
			}
		}

		private class TestClaimsProviderWithTenant : IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send"),
					new Claim(PermissionClaimTypes.TenantIdClaimType, "12345678"),
					new Claim(PermissionClaimTypes.TenantNameClaimType, "test-tenant"),
					new Claim(PermissionClaimTypes.TenantDisplayNameClaimType, "Test Tenant Inc.")
				});
			}
		}

		[Test]
		[TestCase(typeof(TestClaimsProvider))]
		[TestCase(typeof(TestClaimsProviderWithWrongClaimType), 2)]
		[TestCase(typeof(TestClaimsProviderWithDuplicatePermissions))]
		[TestCase(typeof(TestClaimsProviderWithNullResult), 0)]
		[TestCase(typeof(TestClaimsProviderWithTenant), 3, true)]
		public async Task ShouldAddClaimsToPrincipal(Type claimProviderType, int expectedCount = 3, bool hasTenant = false)
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization();
			services.AddClaimsProvider(claimProviderType);
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IClaimsProvider service = serviceProvider.GetRequiredService<IClaimsProvider>();
			IReadOnlyCollection<Claim> claims = await service.GetPermissionClaimsForUserAsync("12345678");

			if(expectedCount == 3)
			{
				claims.HasPermission("Invoices.Read").Should().BeTrue();
				claims.HasPermission("Invoices.Write").Should().BeTrue();
				claims.HasPermission("Invoices.Send").Should().BeTrue();
			}

			if(expectedCount == 2)
			{
				claims.HasPermission("Invoices.Read").Should().BeTrue();
				claims.HasPermission("Invoices.Write").Should().BeFalse();
				claims.HasPermission("Invoices.Send").Should().BeTrue();
			}

			if(hasTenant)
			{
				string tenantId = claims.GetTenantId();
				tenantId.Should().NotBeNullOrWhiteSpace().And.Be("12345678");

				string tenantName = claims.GetTenantName();
				tenantName.Should().NotBeNullOrWhiteSpace().And.Be("test-tenant");

				string tenantDisplayName = claims.GetTenantDisplayName();
				tenantDisplayName.Should().NotBeNullOrWhiteSpace().And.Be("Test Tenant Inc.");
			}

			claims.Where(x => x.Type == PermissionClaimTypes.PermissionClaimType).Should().HaveCount(expectedCount);

			await serviceProvider.DisposeAsync();
		}
	}
}
