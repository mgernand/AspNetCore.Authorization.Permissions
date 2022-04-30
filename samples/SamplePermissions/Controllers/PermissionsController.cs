namespace SamplePermissions.Controllers
{
	using AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Mvc;

	[ApiController]
	[Route("permissions")]
	public class PermissionsController : ControllerBase
	{
		[HttpGet]
		[HasPermission("ShowPermissions")]
		public IActionResult Get()
		{
			var claims = this.User.Claims.Select(x => new { x.Type, x.Value });
			return this.Ok(claims);
		}
	}
}
