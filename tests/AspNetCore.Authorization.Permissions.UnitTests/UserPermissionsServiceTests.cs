namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
    using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;
    using ServiceCollectionExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.ServiceCollectionExtensions;

    [TestFixture]
	public class UserPermissionsServiceTests
	{
		private class TestClaimsProvider : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(Array.Empty<Claim>());
			}
		}

		[Test]
		public void ShouldGetAllAvailablePermissionsFromClaims()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			IReadOnlyCollection<string> result = service.GetPermissionsFrom(principal.Claims);
			result.Should().NotBeNullOrEmpty()
				.And.Contain("Invoices.Read")
				.And.Contain("Invoices.Write")
				.And.Contain("Invoices.Send");
		}

		[Test]
		public void ShouldGetAllAvailablePermissionsFromPrincipal()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			IReadOnlyCollection<string> result = service.GetPermissionsFrom(principal);
			result.Should().NotBeNullOrEmpty()
				.And.Contain("Invoices.Read")
				.And.Contain("Invoices.Write")
				.And.Contain("Invoices.Send");
		}

		[Test]
		public void ShouldGetTenantInfo()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantIdClaimType, "12345678"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantNameClaimType, "test-tenant"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.TenantDisplayNameClaimType, "Test Tenant Inc.")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			string tenantId = service.GetTenantId(principal);
			tenantId.Should().NotBeNull().And.Be("12345678");

			string tenantName = service.GetTenantName(principal);
			tenantName.Should().NotBeNull().And.Be("test-tenant");

			string tenantDisplayName = service.GetTenantDisplayName(principal);
			tenantDisplayName.Should().NotBeNull().And.Be("Test Tenant Inc.");
		}

		[Test]
		public void ShouldHavePermissionFromClaims_WithLoverCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			bool result = service.HasPermission(principal.Claims, "Invoices.Write".ToLowerInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromClaims_WithMixedCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();


			bool result = service.HasPermission(principal.Claims, "invOiCeS.wriTe");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromClaims_WithOriginalNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			bool result = service.HasPermission(principal.Claims, "Invoices.Write");
			result.Should().BeTrue();
		}


		[Test]
		public void ShouldHavePermissionFromClaims_WithUpperCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			bool result = service.HasPermission(principal.Claims, "Invoices.Write".ToUpperInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithLoverCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			bool result = service.HasPermission(principal, "Invoices.Write".ToLowerInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithMixedCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();


			bool result = service.HasPermission(principal, "invOiCeS.wriTe");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithOriginalNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			bool result = service.HasPermission(principal, "Invoices.Write");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithUpperCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IUserPermissionsService>();

			bool result = service.HasPermission(principal, "Invoices.Write".ToUpperInvariant());
			result.Should().BeTrue();
		}
	}
}
