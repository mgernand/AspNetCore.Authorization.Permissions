# AspNetCore.Authorization.Permissions

A libary that add permission-based authorization.

The ASP.NET Core role-based authorization in combination with custom authorization policies
is a good starting point for restricting users' access in an application, but it is very 
static and changes to the meaning of a role or a policy forces you to perform changes in 
your code and to re-deploy your application. This library aims to overcome this limitation.

To be able to dynamically change the access of a user we extend the role in a way that each
role is made up of several fine gained permissions. Throughout the documentation and the 
sample applications we will use the following roles and permissions.

| Role          | Invoice.Read | Invoice.Write | Invoice.Delete | Invoice.Send | Invoice.Payment |
|---------------|--------------|---------------|----------------|--------------|-----------------|
| Boss          | **YES**      | NO            | NO             | NO           | NO              |
| Manager       | **YES**      | NO            | **YES**        | NO           | NO              |
| Employee      | **YES**      | **YES**       | NO             | **YES**      | **YES**         |

In this fictional company the boss can only read invoices, the manager can read and delete invoices and
the employees can read, write, send invocies and can trigger the selllement of the invoice.

If we build the authentication around the three role we will hard-code the access permissions in our
codebase f.e. using the ```[Authorize]``` attribute. But if the boss decides he also wants
to be able to delete invoices, we need to change it in the source code.

Using permissions of the role defined in the table above in the ```[Authorize]``` attribute instead
of the roles, we can just change the role configuration in a data store and assign the permision
**_Invoices.Delete_** to the role **_Boss_**. The boss used is then able to delete invocies without
the need to change the code and re-deploy the application.

The library consists of two parts: 

1. The base definitions, policies and services to be able to check an authenticated users claims 
   (i.e. ```ClaimsPrincipal```) for asigned permissions.
2. An implementation of the permissions API to work with ASP.NET Core Identity.

It is possible to add different storages and claims providers. A library that wanst to provide
the permissions claims just needs to implement the ```IClaimsProvider``` interface and the storage
mechanism of course.

In addition to the basic permissions of users, this library provides an optional multi-tenant feature. 
This feature allows assign tenants to users. The tenant infosmations are then added to the user's
claims. Storage systems can then leverage the tenant information to alter queries (Single Database with
Tenant Column) or to select connection strings (Tenant per Database).

A tenant may have several roles and the permissions of those roles are added to the user's permission
claims. In that way additional permissions can be added to individual claims. The tenant sample applications
provide tenants with a distict tenant role assigned which provide the following additional tenant permissions.
Each tenant represents a separate company. The roles in this example represent different plans of a
SaaS application.

| Role           | Invoice.Statistics | Invoice.TaxExport |
|----------------|--------------------|-------------------|
| Free           | NO                 | NO                |
| Basic          | **YES**            | NO                |
| Professional   | **YES**            | **YES**           |

Using tenants, roles and permissions is a good way to define differnent sets of features, f.e. when creating
different plans of a SaaS application.

## Permission Usage

To configure the permissions with ASP.NET Identity and the default identity models add the following
code to your application startup code. The example uses EF Core and SQLite to store the Identity models.

The users, roles and permissions are added to the storage using the ```ApplicationDbContext``` and EF 
Core migrations. The code is omitted in this document, but you can look it up in the samples code.

```C#
// ... Previous service configuration omitted.

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
	.AddPermissionsIdentity()
	.AddDefaultUI()
	.AddDefaultTokenProviders()
	.AddPermissionsEntityFrameworkStores<ApplicationDbContext>();

// Additional service configuration omitted ...
```

### Restrict access based on permissions

There are several ways to rescript access in your application.

1. Use the ```[Authorize]``` attribute to restrict access to controller actions.
2. Use the ```HasPermission()``` extension method with a ```ClaimsPrincipal``` instance.
3. Use the ```HasPermission()``` method of a ```IUserPermissionsService``` instance.

To retrict the access to an action methods just add the ```[Authorize]``` attribute with the permission
name as contraint.

```C#
// ASP.NET MVC controller action with attribute.
[HttpGet]
[HasPermission("Invoice.Payment")]
public IActionResult Get()
{
	return this.Ok();
}

// Razor Pages with attribute.
[HasPermission("Invoices.Read")]
public class InvoicesReadModel : PageModel
{
	public void OnGet()
	{
	}
}

// Razor Pages with extension method.
public class InvoicesReadModel : PageModel
{
	public IActionResult OnGet()
	{
		if(!this.User.HasPermission("Invoice.Read"))
		{
			if(this.User.IsAuthenticated())
			{
				return this.Forbid();
			}

			return this.Challenge();
		}

		return this.Page();
	}
}
```

## Tenant Usage

TODO: ITenantContextAccessor



