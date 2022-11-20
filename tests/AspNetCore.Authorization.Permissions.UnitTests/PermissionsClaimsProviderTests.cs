namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Security.Claims;
	using System.Threading.Tasks;
    using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;
    using ClaimsEnumerableExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsEnumerableExtensions;
    using ServiceCollectionExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.ServiceCollectionExtensions;

    [TestFixture]
	public class PermissionsClaimsProviderTests
	{
		private class TestClaimsProvider : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
				});
			}
		}

		private class TestClaimsProviderWithDuplicatePermissions : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
				});
			}
		}

		private class TestClaimsProviderWithWrongClaimType : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(ClaimTypes.Role, "Invoices.Write"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
				});
			}
		}

		private class TestClaimsProviderWithNullResult : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(null);
			}
		}

		private class TestClaimsProviderWithTenant : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(new Claim[]
				{
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantIdClaimType, "12345678"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantNameClaimType, "test-tenant"),
					new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantDisplayNameClaimType, "Test Tenant Inc.")
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
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider(services, claimProviderType);
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider>();
			IReadOnlyCollection<Claim> claims = await service.GetPermissionClaimsForUserAsync("12345678");

			if(expectedCount == 3)
			{
				ClaimsEnumerableExtensions.HasPermission(claims, "Invoices.Read").Should().BeTrue();
				ClaimsEnumerableExtensions.HasPermission(claims, "Invoices.Write").Should().BeTrue();
				ClaimsEnumerableExtensions.HasPermission(claims, "Invoices.Send").Should().BeTrue();
			}

			if(expectedCount == 2)
			{
				ClaimsEnumerableExtensions.HasPermission(claims, "Invoices.Read").Should().BeTrue();
				ClaimsEnumerableExtensions.HasPermission(claims, "Invoices.Write").Should().BeFalse();
				ClaimsEnumerableExtensions.HasPermission(claims, "Invoices.Send").Should().BeTrue();
			}

			if(hasTenant)
			{
				string tenantId = ClaimsEnumerableExtensions.GetTenantId(claims);
				tenantId.Should().NotBeNullOrWhiteSpace().And.Be("12345678");

				string tenantName = ClaimsEnumerableExtensions.GetTenantName(claims);
				tenantName.Should().NotBeNullOrWhiteSpace().And.Be("test-tenant");

				string tenantDisplayName = ClaimsEnumerableExtensions.GetTenantDisplayName(claims);
				tenantDisplayName.Should().NotBeNullOrWhiteSpace().And.Be("Test Tenant Inc.");
			}

			claims.Where(x => x.Type == MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType).Should().HaveCount(expectedCount);

			await serviceProvider.DisposeAsync();
		}
	}
}
