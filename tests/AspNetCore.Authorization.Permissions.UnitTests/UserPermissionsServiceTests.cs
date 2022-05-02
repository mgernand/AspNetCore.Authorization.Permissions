﻿namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class UserPermissionsServiceTests
	{
		private class TestClaimsProvider : IClaimsProvider
		{
			/// <inheritdoc />
			public async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Array.Empty<Claim>();
			}
		}

		[Test]
		public void ShouldGetAllAvailablePermissionsFromClaims()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

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
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			IReadOnlyCollection<string> result = service.GetPermissionsFrom(principal);
			result.Should().NotBeNullOrEmpty()
				.And.Contain("Invoices.Read")
				.And.Contain("Invoices.Write")
				.And.Contain("Invoices.Send");
		}


		[Test]
		public void ShouldGetTenantName()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.TenantNameClaimType, "test-tenant"),
				new Claim(PermissionClaimTypes.TenantDisplayNameClaimType, "Test Tenant Inc.")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

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
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			bool result = service.HasPermission(principal.Claims, "Invoices.Write".ToLowerInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromClaims_WithMixedCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();


			bool result = service.HasPermission(principal.Claims, "invOiCeS.wriTe");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromClaims_WithOriginalNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			bool result = service.HasPermission(principal.Claims, "Invoices.Write");
			result.Should().BeTrue();
		}


		[Test]
		public void ShouldHavePermissionFromClaims_WithUpperCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			bool result = service.HasPermission(principal.Claims, "Invoices.Write".ToUpperInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithLoverCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			bool result = service.HasPermission(principal, "Invoices.Write".ToLowerInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithMixedCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();


			bool result = service.HasPermission(principal, "invOiCeS.wriTe");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithOriginalNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			bool result = service.HasPermission(principal, "Invoices.Write");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermissionFromPrincipal_WithUpperCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization(builder =>
			{
				builder.AddClaimsProvider<TestClaimsProvider>();
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();
			IUserPermissionsService service = serviceProvider.GetRequiredService<IUserPermissionsService>();

			bool result = service.HasPermission(principal, "Invoices.Write".ToUpperInvariant());
			result.Should().BeTrue();
		}
	}
}
