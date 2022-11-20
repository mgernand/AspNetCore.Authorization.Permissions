namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
    using FluentAssertions;
	using Microsoft.AspNetCore.Authorization;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;
    using ServiceCollectionExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.ServiceCollectionExtensions;

    [TestFixture]
	public class PermissionAuthorizationPolicyTests
	{
		private class TestClaimsProvider : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
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
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
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
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
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
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
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
