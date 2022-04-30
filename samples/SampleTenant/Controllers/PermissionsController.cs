namespace SampleTenant.Controllers
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("permissions")]
	public class PermissionsController : ControllerBase
	{
		[HttpGet]
		[HasPermission("ShowPermissions")] // The permission comes from a user role.
		public IActionResult Get()
		{
			var claims = this.User.Claims.Select(x => new { x.Type, x.Value });
			return this.Ok(claims);
		}

		[HttpGet("free")]
		[HasPermission("ShowFreePermissions")] // The permission comes from a tenant role.
		public IActionResult GetFree()
		{
			var claims = this.User.Claims.Select(x => new { x.Type, x.Value });
			return this.Ok(claims);
		}
	}
}
