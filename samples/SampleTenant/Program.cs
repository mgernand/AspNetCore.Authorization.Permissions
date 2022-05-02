using AspNetCore.Authorization.Permissions;
using AspNetCore.Authorization.Permissions.Identity;
using AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SampleTenant;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization();
builder.Services.AddPermissionsAuthorization(options =>
{
	options.AddIdentityClaimsProvider();
});

builder.Services
	.AddDbContext<ApplicationDbContext>(options =>
	{
		options.UseSqlite("Filename=permissions.db");
	})
	.AddPermissionsIdentity(options =>
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
	.AddPermissionsEntityFrameworkStores<ApplicationDbContext, TenantAccessorFromUser>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();
