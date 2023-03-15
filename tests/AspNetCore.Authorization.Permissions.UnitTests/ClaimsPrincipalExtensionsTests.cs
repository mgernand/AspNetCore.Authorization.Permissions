namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System.Collections.Generic;
	using System.Security.Claims;
	using FluentAssertions;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using NUnit.Framework;

	[TestFixture]
	public class ClaimsPrincipalExtensionsTests
	{
		[Test]
		public void ShouldGetAllAvailablePermissions()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			IReadOnlyCollection<string> result = principal.GetPermissions();
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
				new Claim(PermissionClaimTypes.TenantIdClaimType, "12345678"),
				new Claim(PermissionClaimTypes.TenantNameClaimType, "test-tenant"),
				new Claim(PermissionClaimTypes.TenantDisplayNameClaimType, "Test Tenant Inc.")
			}));

			string tenantId = principal.GetTenantId();
			tenantId.Should().NotBeNull().And.Be("12345678");

			string tenantName = principal.GetTenantName();
			tenantName.Should().NotBeNull().And.Be("test-tenant");

			string tenantDisplayName = principal.GetTenantDisplayName();
			tenantDisplayName.Should().NotBeNull().And.Be("Test Tenant Inc.");
		}

		[Test]
		public void ShouldGetUserId()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(ClaimTypes.NameIdentifier, "123456")
			}));

			string result = principal.GetUserId();

			result.Should().NotBeNull().And.Be("123456");
		}

		[Test]
		public void ShouldHavePermission_WithLoverCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = principal.HasPermission("Invoices.Write".ToLowerInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermission_WithMixedCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = principal.HasPermission("invOiCeS.wriTe");
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldHavePermission_WithOriginalNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = principal.HasPermission("Invoices.Write");
			result.Should().BeTrue();
		}


		[Test]
		public void ShouldHavePermission_WithUpperCaseNameFormatting()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity(new List<Claim>
			{
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Read"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Write"),
				new Claim(PermissionClaimTypes.PermissionClaimType, "Invoices.Send")
			}));

			bool result = principal.HasPermission("Invoices.Write".ToUpperInvariant());
			result.Should().BeTrue();
		}

		[Test]
		public void ShouldNotGetUserId()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity());

			string result = principal.GetUserId();

			result.Should().BeNull();
		}


		[Test]
		public void ShouldNotTenantName()
		{
			ClaimsPrincipal principal = new ClaimsPrincipal(new ClaimsIdentity());

			string result = principal.GetTenantName();

			result.Should().BeNull();
		}
	}
}
