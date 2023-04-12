using MadEyeMatt.AspNetCore.Authorization.Permissions;
using MadEyeMatt.AspNetCore.Identity.MongoDB;
using MadEyeMatt.AspNetCore.Identity.Permissions;
using MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using System.Threading;
using System;
using System.Collections.Generic;
using MadEyeMatt.MongoDB.DbContext;
using MadEyeMatt.MongoDB.DbContext.Initialization;
using MongoDB.Driver;
using MongoSampleTenant;
using MongoSampleTenant.Model;

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
		options.UseDatabase("mongodb://localhost:27017", "permissions");
	})
	.AddPermissionsIdentityCore<MongoIdentityTenant, MongoIdentityTenantUser, MongoIdentityRole, MongoIdentityPermission>(options =>
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
	.AddDefaultTenantProvider()
	.AddTenantManager<AspNetTenantManager<MongoIdentityTenant>>()
	.AddUserManager<AspNetUserManager<MongoIdentityTenantUser>>()
	.AddRoleManager<AspNetRoleManager<MongoIdentityRole>>()
	.AddPermissionManager<AspNetPermissionManager<MongoIdentityPermission>>()
	.AddPermissionsMongoDbStores<InvoicesContext>();

builder.Services.AddEnsureSchema<EnsureInvoiceSchema<InvoicesContext>>();

WebApplication app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapRazorPages();

// Insert sample data
using (IServiceScope serviceScope = app.Services.CreateScope())
{
	await serviceScope.ServiceProvider.InitializeMongoDbIdentityStores();

	try
    {
        // Insert roles
        IRoleStore<MongoIdentityRole> roleStore = serviceScope.ServiceProvider.GetRequiredService<IRoleStore<MongoIdentityRole>>();

		// User roles.
        string bossRoleId = ObjectId.GenerateNewId().ToString();
        await roleStore.CreateAsync(new MongoIdentityRole
        {
            Id = bossRoleId,
            Name = "Boss",
            NormalizedName = "BOSS"
        }, CancellationToken.None);

        string managerRoleId = ObjectId.GenerateNewId().ToString();
        await roleStore.CreateAsync(new MongoIdentityRole
        {
            Id = managerRoleId,
            Name = "Manager",
            NormalizedName = "MANAGER"
        }, CancellationToken.None);

        string employeeRoleId = ObjectId.GenerateNewId().ToString();
        await roleStore.CreateAsync(new MongoIdentityRole
        {
            Id = employeeRoleId,
            Name = "Employee",
            NormalizedName = "EMPLOYEE"
        }, CancellationToken.None);

        // Tenant roles.
		string freeRoleId = ObjectId.GenerateNewId().ToString();
		await roleStore.CreateAsync(new MongoIdentityRole
		{
			Id = freeRoleId,
			Name = "Free",
			NormalizedName = "FREE"
        }, CancellationToken.None);

		string basicRoleId = ObjectId.GenerateNewId().ToString();
		await roleStore.CreateAsync(new MongoIdentityRole
		{
			Id = basicRoleId,
			Name = "Basic",
			NormalizedName = "BASIC"
        }, CancellationToken.None);

		string professionalRoleId = ObjectId.GenerateNewId().ToString();
		await roleStore.CreateAsync(new MongoIdentityRole
		{
			Id = professionalRoleId,
			Name = "Professional",
			NormalizedName = "PROFESSIONAL"
        }, CancellationToken.None);

        // Insert tenants.
		ITenantStore<MongoIdentityTenant> tenantStore = serviceScope.ServiceProvider.GetRequiredService<ITenantStore<MongoIdentityTenant>>();

		string startupTenantId = ObjectId.GenerateNewId().ToString();
		await tenantStore.CreateAsync(new MongoIdentityTenant
		{
			Id = startupTenantId,
			Name = "Startup",
			NormalizedName = "STARTUP",
			DisplayName = "Startup LLC.",
			Roles = { freeRoleId }
		}, CancellationToken.None);

		string companyTenantId = ObjectId.GenerateNewId().ToString();
		await tenantStore.CreateAsync(new MongoIdentityTenant
		{
			Id = companyTenantId,
			Name = "Company",
			NormalizedName = "COMPANY",
			DisplayName = "Company Inc.",
			Roles = { basicRoleId }
        }, CancellationToken.None);

		string corporateTenantId = ObjectId.GenerateNewId().ToString();
		await tenantStore.CreateAsync(new MongoIdentityTenant
		{
			Id = corporateTenantId,
			Name = "Corporate",
			NormalizedName = "CORPORATE",
			DisplayName = "Corporate Corp.",
			Roles = { professionalRoleId }
        }, CancellationToken.None);

        // Insert users.
		IUserStore<MongoIdentityTenantUser> userStore = serviceScope.ServiceProvider.GetRequiredService<IUserStore<MongoIdentityTenantUser>>();

        // The password for every user: 123456
		// Tenant: Startup
        await userStore.CreateAsync(new MongoIdentityTenantUser
        {
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "boss@startup",
			NormalizedUserName = "BOSS@STARTUP",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = startupTenantId,
			Roles = { bossRoleId }
		}, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityTenantUser
        {
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "manager@startup",
			NormalizedUserName = "MANAGER@STARTUP",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = startupTenantId,
			Roles = { managerRoleId }
		}, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityTenantUser
        {
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "employee@startup",
			NormalizedUserName = "EMPLOYEE@STARTUP",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = startupTenantId,
			Roles = { employeeRoleId }
		}, CancellationToken.None);

        // Tenant: Company
		await userStore.CreateAsync(new MongoIdentityTenantUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "boss@company",
			NormalizedUserName = "BOSS@COMPANY",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = companyTenantId,
			Roles = { bossRoleId }
        }, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityTenantUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "manager@company",
			NormalizedUserName = "MANAGER@COMPANY",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = companyTenantId,
			Roles = { managerRoleId }
        }, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityTenantUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "employee@company",
			NormalizedUserName = "EMPLOYEE@COMPANY",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = companyTenantId,
			Roles = { employeeRoleId }
        }, CancellationToken.None);

        // Tenant: Corporate
		await userStore.CreateAsync(new MongoIdentityTenantUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "boss@corporate",
			NormalizedUserName = "BOSS@CORPORATE",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = corporateTenantId,
			Roles = { bossRoleId }
        }, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityTenantUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "manager@corporate",
			NormalizedUserName = "MANAGER@CORPORATE",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = corporateTenantId,
			Roles = { managerRoleId }
        }, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityTenantUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "employee@corporate",
			NormalizedUserName = "EMPLOYEE@CORPORATE",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			TenantId = corporateTenantId,
			Roles = { employeeRoleId }
        }, CancellationToken.None);

		// Insert permissions.
		IPermissionStore<MongoIdentityPermission> permissionStore = serviceScope.ServiceProvider.GetRequiredService<IPermissionStore<MongoIdentityPermission>>();

		// User permissions.
		await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.Read",
			NormalizedName = "INVOICE.READ",
			Roles = { bossRoleId, managerRoleId, employeeRoleId }
		}, CancellationToken.None);

		await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.Write",
			NormalizedName = "INVOICE.WRITE",
			Roles = { employeeRoleId }
		}, CancellationToken.None);

		await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.Delete",
			NormalizedName = "INVOICE.DELETE",
			Roles = { managerRoleId }
		}, CancellationToken.None);

		await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.Send",
			NormalizedName = "INVOICE.SEND",
			Roles = { employeeRoleId }
		}, CancellationToken.None);

		await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.Payment",
			NormalizedName = "INVOICE.PAYMENT",
			Roles = { employeeRoleId }
		}, CancellationToken.None);

        // Tenant permissions.

        // Free role permissions => none
        // Basic role permissions
		// Professional role permissions

        await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.Statistics",
			NormalizedName = "INVOICE.STATISTICS",
			Roles = { basicRoleId, professionalRoleId }
		}, CancellationToken.None);

		await permissionStore.CreateAsync(new MongoIdentityPermission
		{
			Id = ObjectId.GenerateNewId().ToString(),
			Name = "Invoice.TaxExport",
			NormalizedName = "INVOICE.TAXEXPORT",
			Roles = { professionalRoleId }
		}, CancellationToken.None);

		// Insert invoices.
		InvoicesContext context = serviceScope.ServiceProvider.GetRequiredService<InvoicesContext>();
		IMongoCollection<Invoice> collection = context.GetCollection<Invoice>();

		// Startup invoices
        collection.InsertMany(new List<Invoice>
		{
			new Invoice
			{
				Total = 99.95m,
				Note = "This is a Startup invoice.",
				TenantID = startupTenantId
            },
			new Invoice
			{
				Total = 99.95m,
				Note = "This is a Startup invoice.",
				TenantID = startupTenantId
            },
			new Invoice
			{
				Total = 99.95m,
				Note = "This is a Startup invoice.",
				TenantID = startupTenantId
            }
		});

        // Company invoices
		collection.InsertMany(new List<Invoice>
		{
			new Invoice
			{
				Total = 199.95m,
				Note = "This is a Company invoice.",
                TenantID = companyTenantId
			},
			new Invoice
			{
				Total = 199.95m,
				Note = "This is a Company invoice.",
				TenantID = companyTenantId
            },
			new Invoice
			{
				Total = 199.95m,
				Note = "This is a Company invoice.",
				TenantID = companyTenantId
            }
		});

        // Corporate invoices
		collection.InsertMany(new List<Invoice>
		{
			new Invoice
			{
				Total = 399.95m,
				Note = "This is a Corporate invoice.",
                TenantID = corporateTenantId
			},
			new Invoice
			{
				Total = 399.95m,
				Note = "This is a Corporate invoice.",
				TenantID = corporateTenantId
            },
			new Invoice
			{
				Total = 399.95m,
				Note = "This is a Corporate invoice.",
				TenantID = corporateTenantId
            }
		});
    }
    catch
    {
        Console.WriteLine(@"Data already exists.");
    }
}

app.Run();