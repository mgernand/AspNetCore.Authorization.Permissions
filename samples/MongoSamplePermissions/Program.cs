using MadEyeMatt.AspNetCore.Authorization.Permissions;
using MadEyeMatt.AspNetCore.Identity.MongoDB;
using MadEyeMatt.AspNetCore.Identity.Permissions;
using MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoSamplePermissions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization();
builder.Services.AddPermissionsAuthorization();

builder.Services
	.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddIdentityCookies();

builder.Services
	.AddMongoDbContext<InvoicesContext>(options =>
	{
		options.ConnectionString = "mongodb://localhost:27017";
		options.DatabaseName = "permissions";
	})
	.AddPermissionsIdentityCore<MongoIdentityUser, MongoIdentityRole, MongoIdentityPermission>(options =>
	{
		options.Password.RequireDigit = false;
		options.Password.RequireLowercase = false;
		options.Password.RequireNonAlphanumeric = false;
		options.Password.RequireUppercase = false;
		options.Password.RequiredLength = 6;
		options.Password.RequiredUniqueChars = 0;
	})
	.AddDefaultUI()
	.AddDefaultTokenProviders()
	.AddPermissionClaimsProvider()
	.AddUserManager<AspNetUserManager<MongoIdentityUser>>()
	.AddRoleManager<AspNetRoleManager<MongoIdentityRole>>()
	.AddPermissionManager<AspNetPermissionManager<MongoIdentityPermission>>()
	.AddPermissionsMongoDbStores<InvoicesContext>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

await app.InitializeMongoDbStores();

app.Run();
