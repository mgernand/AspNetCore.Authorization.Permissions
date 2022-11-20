namespace SamplePermissions
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
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			base.OnModelCreating(builder);

			builder.HasDefaultSchema("identity");

			builder.Entity<PermissionsUser>(entity =>
			{
				entity.ToTable("Users");

				// The password for every user: 123456
				entity.HasData(new PermissionsUser
				{
					Id = "a0f112af-5e39-4b3f-bc50-015591861ec0",
					UserName = "boss@company",
					NormalizedUserName = "BOSS@COMPANY",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA=="
				});
				entity.HasData(new PermissionsUser
				{
					Id = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
					UserName = "manager@company",
					NormalizedUserName = "MANAGER@COMPANY",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA=="
				});
				entity.HasData(new PermissionsUser
				{
					Id = "04517a45-d6f5-4993-888b-04c924902b3a",
					UserName = "employee@company",
					NormalizedUserName = "EMPLOYEE@COMPANY",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA=="
				});
			});

			builder.Entity<PermissionsRole>(entity =>
			{
				entity.ToTable("Roles");
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
			});

			builder.Entity<PermissionsPermission>(entity =>
			{
				entity.ToTable("Permissions");
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
			});

			builder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable("UserRoles");

				// Boss
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "a0f112af-5e39-4b3f-bc50-015591861ec0",
					RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
				});

				// Manager
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
					RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
				});

				// Employee
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "04517a45-d6f5-4993-888b-04c924902b3a",
					RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
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
			});

			builder.Entity<PermissionsTenant<string>>(entity => entity.ToTable("Tenants"));
			builder.Entity<IdentityUserClaim<string>>(entity => entity.ToTable("UserClaims"));
			builder.Entity<IdentityUserLogin<string>>(entity => entity.ToTable("UserLogins"));
			builder.Entity<IdentityRoleClaim<string>>(entity => entity.ToTable("RoleClaims"));
			builder.Entity<IdentityUserToken<string>>(entity => entity.ToTable("UserTokens"));
			builder.Entity<IdentityTenantRole<string>>(entity => entity.ToTable("TenantRoles"));
		}
	}
}
