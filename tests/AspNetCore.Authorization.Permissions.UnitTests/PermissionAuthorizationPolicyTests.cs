namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Threading.Tasks;
	using FluentAssertions;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class PermissionAuthorizationPolicyTests
	{
		[Test]
		public async Task ShouldGetPolicyForPermission()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPermissions();
			services.Configure<AuthorizationOptions>(options =>
			{
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IAuthorizationPolicyProvider service = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>();
			service.Should().NotBeNull();

			AuthorizationPolicy policy = await service.GetPolicyAsync("Invoices.Read");
			policy.Should().NotBeNull();
			policy.Requirements.Should().NotBeNullOrEmpty();
			policy.Requirements.Should().HaveCount(1);
		}

		[Test]
		public async Task ShouldThrowArgumentException()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPermissions();
			services.Configure<AuthorizationOptions>(options =>
			{
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IAuthorizationPolicyProvider service = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>();
			service.Should().NotBeNull();

			Func<Task> func = async () => await service.GetPolicyAsync(string.Empty);
			await func.Should().ThrowAsync<ArgumentException>();
		}

		[Test]
		public async Task ShouldThrowArgumentNullException()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPermissions();
			services.Configure<AuthorizationOptions>(options =>
			{
			});
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IAuthorizationPolicyProvider service = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>();
			service.Should().NotBeNull();

			Func<Task> func = async () => await service.GetPolicyAsync(null);
			await func.Should().ThrowAsync<ArgumentNullException>();
		}
	}
}
