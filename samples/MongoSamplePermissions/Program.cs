using System;
using System.Collections.Generic;
using System.Threading;
using MadEyeMatt.AspNetCore.Authorization.Permissions;
using MadEyeMatt.AspNetCore.Identity.MongoDB;
using MadEyeMatt.AspNetCore.Identity.Permissions;
using MadEyeMatt.AspNetCore.Identity.Permissions.MongoDB;
using MadEyeMatt.Extensions.Identity.Permissions;
using MadEyeMatt.MongoDB.DbContext;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoSamplePermissions;
using MongoSamplePermissions.Model;

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
using(IServiceScope serviceScope = app.Services.CreateScope())
{
	await serviceScope.ServiceProvider.InitializeMongoDbIdentityStores();

	try
	{
		// Insert roles
		IRoleStore<MongoIdentityRole> roleStore = serviceScope.ServiceProvider.GetRequiredService<IRoleStore<MongoIdentityRole>>();

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

		// Insert users
		IUserStore<MongoIdentityUser> userStore = serviceScope.ServiceProvider.GetRequiredService<IUserStore<MongoIdentityUser>>();

		// The password for every user: 123456
		await userStore.CreateAsync(new MongoIdentityUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "boss@company",
			NormalizedUserName = "BOSS@COMPANY",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			Roles = { bossRoleId }
		}, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "manager@company",
			NormalizedUserName = "MANAGER@COMPANY",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			Roles = { managerRoleId }
		}, CancellationToken.None);

		await userStore.CreateAsync(new MongoIdentityUser
		{
			Id = ObjectId.GenerateNewId().ToString(),
			UserName = "employee@company",
			NormalizedUserName = "EMPLOYEE@COMPANY",
			PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
			Roles = { employeeRoleId }
		}, CancellationToken.None);

		// Insert permissions
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

		// Insert invoices.
		InvoicesContext context = serviceScope.ServiceProvider.GetRequiredService<InvoicesContext>();
		IMongoCollection<Invoice> collection = context.GetCollection<Invoice>();

		collection.InsertMany(new List<Invoice>
		{
			new Invoice
			{
				Total = 199.95m,
				Note = "This is a Company invoice."
			},
			new Invoice
			{
				Total = 199.95m,
				Note = "This is a Company invoice."
			},
			new Invoice
			{
				Total = 199.95m,
				Note = "This is a Company invoice."
			}
        });
	}
	catch
	{
		Console.WriteLine(@"Data already exists.");
	}
}

app.Run();
