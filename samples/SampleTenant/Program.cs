using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SampleTenant;
using IdentityBuilderExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.IdentityBuilderExtensions;
using ServiceCollectionExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.ServiceCollectionExtensions;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddRazorPages();

builder.Services.AddAuthorization();
ServiceCollectionExtensions.AddPermissionsAuthorization(builder.Services);

builder.Services
	.AddAuthentication(IdentityConstants.ApplicationScheme)
	.AddIdentityCookies();

MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.EntityFrameworkCore.IdentityBuilderExtensions.AddPermissionsEntityFrameworkStores<ApplicationDbContext, MadEyeMatt.AspNetCore.Authorization.Permissions.HttpContextUserTenantProvider>(IdentityBuilderExtensions.AddIdentityClaimsProvider(MadEyeMatt.AspNetCore.Authorization.Permissions.Identity.ServiceCollectionExtensions.AddPermissionsIdentity(builder.Services
            .AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite("Filename=permissions.db");
            })
            .AddDbContext<InvoicesDbContext>(options =>
            {
                options.UseSqlite("Filename=permissions.db");
            }), options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequiredUniqueChars = 0;
        })
        .AddDefaultUI()
        .AddDefaultTokenProviders()));

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();
app.Run();
