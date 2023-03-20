namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using FluentAssertions;
	using MadEyeMatt.AspNetCore.Authorization.Permissions;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class PermissionAuthorizationPolicyTests
	{
		private class TestClaimsProvider : IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(Array.Empty<Claim>());
			}
		}

		[Test]
		public async Task ShouldGetPolicyForPermission()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization();
			services.AddClaimsProvider<TestClaimsProvider>();
			services.Configure<AuthorizationOptions>(options =>
			{
			});
			IServiceProvider serviceProvider = services.BuildServiceProvider();

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
			services.AddPermissionsAuthorization();
			services.AddClaimsProvider<TestClaimsProvider>();
			services.Configure<AuthorizationOptions>(options =>
			{
			});
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			IAuthorizationPolicyProvider service = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>();
			service.Should().NotBeNull();

			Func<Task> func = async () => await service.GetPolicyAsync(string.Empty);
			await func.Should().ThrowAsync<ArgumentException>();
		}

		[Test]
		public async Task ShouldThrowArgumentNullException()
		{
			IServiceCollection services = new ServiceCollection();
			services.AddPermissionsAuthorization();
			services.AddClaimsProvider<TestClaimsProvider>();
			services.Configure<AuthorizationOptions>(options =>
			{
			});
			IServiceProvider serviceProvider = services.BuildServiceProvider();

			IAuthorizationPolicyProvider service = serviceProvider.GetRequiredService<IAuthorizationPolicyProvider>();
			service.Should().NotBeNull();

			Func<Task> func = async () => await service.GetPolicyAsync(null);
			await func.Should().ThrowAsync<ArgumentNullException>();
		}
	}
}
