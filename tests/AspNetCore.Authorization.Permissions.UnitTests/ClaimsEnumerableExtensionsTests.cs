namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System.Collections.Generic;
	using System.Security.Claims;
    using FluentAssertions;
	using NUnit.Framework;
    using ClaimsEnumerableExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.ClaimsEnumerableExtensions;

    [TestFixture]
	public class ClaimsEnumerableExtensionsTests
	{
		[Test]
		public void ShouldGetAllAvailablePermissions()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IReadOnlyCollection<string> result = ClaimsEnumerableExtensions.GetPermissions(principal.Claims);
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

			string tenantId = ClaimsEnumerableExtensions.GetTenantId(principal.Claims);
			tenantId.Should().NotBeNull().And.Be("12345678");

			string tenantName = ClaimsEnumerableExtensions.GetTenantName(principal.Claims);
			tenantName.Should().NotBeNull().And.Be("test-tenant");

			string tenantDisplayName = ClaimsEnumerableExtensions.GetTenantDisplayName(principal.Claims);
			tenantDisplayName.Should().NotBeNull().And.Be("Test Tenant Inc.");
		}

		[Test]
		public void ShouldGetUserId()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, "123456")
			}));

			string result = ClaimsEnumerableExtensions.GetUserId(principal.Claims);

			result.Should().NotBeNull().And.Be("123456");
		}

		[Test]
		public void ShouldHavePermission_WithLoverCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = ClaimsEnumerableExtensions.HasPermission(principal.Claims, "Invoices.Write".ToLowerInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermission_WithMixedCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = ClaimsEnumerableExtensions.HasPermission(principal.Claims, "invOiCeS.wriTe");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermission_WithOriginalNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = ClaimsEnumerableExtensions.HasPermission(principal.Claims, "Invoices.Write");
			result.Should().BeTrue();
		}


		[Test]
		public void ShouldHavePermission_WithUpperCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = ClaimsEnumerableExtensions.HasPermission(principal.Claims, "Invoices.Write".ToUpperInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldNotGetUserId()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity());

			string result = ClaimsEnumerableExtensions.GetUserId(principal.Claims);

			result.Should().BeNull();
		}

		[Test]
		public void ShouldNotTenantName()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity());

			string result = ClaimsEnumerableExtensions.GetTenantName(principal.Claims);

			result.Should().BeNull();
		}
	}
}
