namespace SamplePermissions
{
	using AspNetCore.Authorization.Permissions.Identity;
	using AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore;
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

			builder.Entity<IdentityTenantUser>(entity =>
			{
				entity.ToTable("Users");
				entity.HasData(new IdentityTenantUser
				{
					Id = "127c0027-0d04-47f2-8a08-83a31bead094",
					UserName = "m.gernand@fluxera.com",
					NormalizedUserName = "M.GERNAND@FLUXERA.COM",
					Email = "m.gernand@fluxera.com",
					NormalizedEmail = "M.GERNAND@FLUXERA.COM",
					PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA=="
				});
			});

			builder.Entity<IdentityTenant>(entity =>
			{
				entity.ToTable("Tenants");
			});

			builder.Entity<IdentityRole>(entity =>
			{
				entity.ToTable("Roles");
				entity.HasData(new IdentityRole
				{
					Id = "8cefbfb9-6ea6-4c11-bb60-b82352096e79",
					Name = "Administrator",
					NormalizedName = "ADMINISTRATOR"
				});
			});

			builder.Entity<IdentityPermission>(entity =>
			{
				entity.ToTable("Permissions");
				entity.HasData(new IdentityPermission
				{
					Id = "10445310-d35d-4158-850e-9c8c271efb49",
					Name = "ShowPermissions",
					NormalizedName = "SHOWPERMISSIONS"
				});
			});

			builder.Entity<IdentityUserRole<string>>(entity =>
			{
				entity.ToTable("UserRoles");
				entity.HasData(new IdentityUserRole<string>
				{
					UserId = "127c0027-0d04-47f2-8a08-83a31bead094",
					RoleId = "8cefbfb9-6ea6-4c11-bb60-b82352096e79"
				});
			});

			builder.Entity<IdentityUserClaim<string>>(entity =>
			{
				entity.ToTable("UserClaims");
			});

			builder.Entity<IdentityUserLogin<string>>(entity =>
			{
				entity.ToTable("UserLogins");
			});

			builder.Entity<IdentityRoleClaim<string>>(entity =>
			{
				entity.ToTable("RoleClaims");
			});

			builder.Entity<IdentityUserToken<string>>(entity =>
			{
				entity.ToTable("UserTokens");
			});

			builder.Entity<IdentityRolePermission<string>>(entity =>
			{
				entity.ToTable("RolePermissions");
				entity.HasData(new IdentityRolePermission<string>
				{
					PermissionId = "10445310-d35d-4158-850e-9c8c271efb49",
					RoleId = "8cefbfb9-6ea6-4c11-bb60-b82352096e79"
				});
			});

			builder.Entity<IdentityTenantRole<string>>(entity =>
			{
				entity.ToTable("TenantRoles");
			});
		}
	}
}
