namespace SampleTenant
{
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore;
	using MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.Model;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;

	public class ApplicationDbContext : IdentityPermissionsDbContext
	{
		/// <summary>
		///     Initializes a new instance of <see cref="ApplicationDbContext" />.
		/// </summary>
		/// <param name="options">The options to be used by a <see cref="DbContext" />.</param>
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema("identity");

			builder.Entity<PermissionsTenant>(entity =>
			{
				entity.ToTable("Tenants");

				// Tenant: Startup
				entity.HasData(new PermissionsTenant
				{
					Id = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
					Name = "Startup",
					NormalizedName = "STARTUP",
					DisplayName = "Startup LLC."
				});

				// Tenant: Company
				entity.HasData(new PermissionsTenant
				{
					Id = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
					Name = "Company",
					NormalizedName = "COMPANY",
					DisplayName = "Company Inc."
				});

				// Tenant: Corporate
				entity.HasData(new PermissionsTenant
				{
					Id = "49a049d2-23ad-41df-8806-240aebaa2f17",
					Name = "Corporate",
					NormalizedName = "CORPORATE",
					DisplayName = "Corporate Corp."
				});
			});

			builder.Entity<PermissionsUser>(entity =>
			{
				entity.ToTable("Users");

				// The password for every user: 123456

				// Tenant: Startup
				entity.HasData(new PermissionsUser
				{
					Id = "ea346013-ec20-4a69-8a60-8684ffb58a5f",
					UserName = "boss@startup",
					NormalizedUserName = "BOSS@STARTUP",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
				});
				entity.HasData(new PermissionsUser
				{
					Id = "50cd8ad5-b945-4541-90c9-156f6940c18b",
					UserName = "manager@startup",
					NormalizedUserName = "MANAGER@STARTUP",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
				});
				entity.HasData(new PermissionsUser
				{
					Id = "142838fe-7e64-484b-a769-87b327726715",
					UserName = "employee@startup",
					NormalizedUserName = "EMPLOYEE@STARTUP",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2"
				});

				// Tenant: Company
				entity.HasData(new PermissionsUser
				{
					Id = "a0f112af-5e39-4b3f-bc50-015591861ec0",
					UserName = "boss@company",
					NormalizedUserName = "BOSS@COMPANY",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
				});
				entity.HasData(new PermissionsUser
				{
					Id = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
					UserName = "manager@company",
					NormalizedUserName = "MANAGER@COMPANY",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
				});
				entity.HasData(new PermissionsUser
				{
					Id = "04517a45-d6f5-4993-888b-04c924902b3a",
					UserName = "employee@company",
					NormalizedUserName = "EMPLOYEE@COMPANY",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46"
				});

				// Tenant: Corporate
				entity.HasData(new PermissionsUser
				{
					Id = "dbcf2449-14b7-4766-9829-ae65604500b0",
					UserName = "boss@corporate",
					NormalizedUserName = "BOSS@CORPORATE",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
				});
				entity.HasData(new PermissionsUser
				{
					Id = "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55",
					UserName = "manager@corporate",
					NormalizedUserName = "MANAGER@CORPORATE",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
				});
				entity.HasData(new PermissionsUser
				{
					Id = "e420f504-d953-4bec-95fd-1613fd760652",
					UserName = "employee@corporate",
					NormalizedUserName = "EMPLOYEE@CORPORATE",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17"
				});
			});

			builder.Entity<PermissionsRole>(entity =>
			{
				entity.ToTable("Roles");

				// User roles.
				entity.HasData(new PermissionsRole
				{
					Id = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483",
					Name = "Boss",
					NormalizedName = "BOSS"
				});
				entity.HasData(new PermissionsRole
				{
					Id = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
					Name = "Manager",
					NormalizedName = "MANAGER"
				});
				entity.HasData(new PermissionsRole
				{
					Id = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
					Name = "Employee",
					NormalizedName = "EMPLOYEE"
				});

				// Tenant roles.
				entity.HasData(new PermissionsRole
				{
					Id = "ecae3c35-0d88-424f-a1bc-31cba5add7a7",
					Name = "Free",
					NormalizedName = "FREE"
				});
				entity.HasData(new PermissionsRole
				{
					Id = "49161cff-c451-4c44-ac59-467883fe1517",
					Name = "Basic",
					NormalizedName = "BASIC"
				});
				entity.HasData(new PermissionsRole
				{
					Id = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
					Name = "Professional",
					NormalizedName = "PROFESSIONAL"
				});
			});

			builder.Entity<PermissionsPermission>(entity =>
			{
				entity.ToTable("Permissions");

				// User permissions.
				entity.HasData(new PermissionsPermission
				{
					Id = "5b9c4926-3dc6-447c-a092-addab890a15f",
					Name = "Invoice.Read",
					NormalizedName = "INVOICE.READ"
				});
				entity.HasData(new PermissionsPermission
				{
					Id = "be5b92e5-c6c6-480b-b235-d4df402a73cc",
					Name = "Invoice.Write",
					NormalizedName = "INVOICE.WRITE"
				});
				entity.HasData(new PermissionsPermission
				{
					Id = "e123b8c0-0646-4075-b73e-07ca9d611c8e",
					Name = "Invoice.Delete",
					NormalizedName = "INVOICE.DELETE"
				});
				entity.HasData(new PermissionsPermission
				{
					Id = "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2",
					Name = "Invoice.Send",
					NormalizedName = "INVOICE.SEND"
				});
				entity.HasData(new PermissionsPermission
				{
					Id = "ef54d62d-a36b-4ab3-b868-f170c0054fac",
					Name = "Invoice.Payment",
					NormalizedName = "INVOICE.PAYMENT"
				});

				// Tenant permissions.
				entity.HasData(new PermissionsPermission
				{
					Id = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed",
					Name = "Invoice.Statistics",
					NormalizedName = "INVOICE.STATISTICS"
				});
				entity.HasData(new PermissionsPermission
				{
					Id = "f1af54df-c9e7-4570-850f-c563732c15b4",
					Name = "Invoice.TaxExport",
					NormalizedName = "INVOICE.TAXEXPORT"
				});
			});

			builder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable("UserRoles");

				// Boss Startup
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "ea346013-ec20-4a69-8a60-8684ffb58a5f",
					RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
				});
				// Boss Company
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "a0f112af-5e39-4b3f-bc50-015591861ec0",
					RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
				});
				// Boss Corporate
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "dbcf2449-14b7-4766-9829-ae65604500b0",
					RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
				});

				// Manager Startup
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "50cd8ad5-b945-4541-90c9-156f6940c18b",
					RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
				});
				// Manager Company
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
					RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
				});
				// Manager Corporate
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55",
					RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
				});

				// Employee Startup
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "142838fe-7e64-484b-a769-87b327726715",
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
				});
				// Employee Company
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "04517a45-d6f5-4993-888b-04c924902b3a",
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
				});
				// Employee Corporate
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "e420f504-d953-4bec-95fd-1613fd760652",
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
				});
			});

			builder.Entity<IdentityTenantRole<string>>(entity =>
			{
				entity.ToTable("TenantRoles");

				// Startup has role Free
				entity.HasData(new IdentityTenantRole<string>
				{
					TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
					RoleId = "ecae3c35-0d88-424f-a1bc-31cba5add7a7"
				});

				// Company has role Basic
				entity.HasData(new IdentityTenantRole<string>
				{
					TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
					RoleId = "49161cff-c451-4c44-ac59-467883fe1517"
				});

				// Corporate has role Professional
				entity.HasData(new IdentityTenantRole<string>
				{
					TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
					RoleId = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230"
				});
			});

			builder.Entity<PermissionsRolePermission<string>>(entity =>
			{
				entity.ToTable("RolePermissions");

				// Boss role permissions
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483",
					PermissionId = "5b9c4926-3dc6-447c-a092-addab890a15f"
				});

				// Manager role permissions
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
					PermissionId = "5b9c4926-3dc6-447c-a092-addab890a15f"
				});
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
					PermissionId = "e123b8c0-0646-4075-b73e-07ca9d611c8e"
				});

				// Employee role permissions
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
					PermissionId = "5b9c4926-3dc6-447c-a092-addab890a15f"
				});
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
					PermissionId = "be5b92e5-c6c6-480b-b235-d4df402a73cc"
				});
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
					PermissionId = "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2"
				});
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
					PermissionId = "ef54d62d-a36b-4ab3-b868-f170c0054fac"
				});

				// Free role permissions => none

				// Basic role permissions
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "49161cff-c451-4c44-ac59-467883fe1517",
					PermissionId = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed"
				});

				// Professional role permissions
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
					PermissionId = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed"
				});
				entity.HasData(new PermissionsRolePermission<string>
				{
					RoleId = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
					PermissionId = "f1af54df-c9e7-4570-850f-c563732c15b4"
				});
			});

			builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
			builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
			builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
			builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
		}
	}
}
