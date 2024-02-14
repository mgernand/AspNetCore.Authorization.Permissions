namespace MadEyeMatt.Extensions.Identity.Permissions
{
	using Microsoft.Extensions.Logging;

	internal static class LoggerEventIds
	{
		public static readonly EventId TenantAlreadyInRole = new EventId(100, nameof(TenantAlreadyInRole));
		public static readonly EventId TenantNotInRole = new EventId(101, nameof(TenantNotInRole));

    }
}
