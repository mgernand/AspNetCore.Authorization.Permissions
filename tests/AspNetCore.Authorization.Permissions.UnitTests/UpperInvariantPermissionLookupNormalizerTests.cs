namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
    using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;
    using ServiceCollectionExtensions = MadEyeMatt.AspNetCore.Authorization.Permissions.ServiceCollectionExtensions;

    [TestFixture]
	public class UpperInvariantPermissionLookupNormalizerTests
	{
		private class TestClaimsProvider : MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IClaimsProvider
		{
			/// <inheritdoc />
			public Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Task.FromResult<IReadOnlyCollection<Claim>>(Array.Empty<Claim>());
			}
		}

		public static IEnumerable<object[]> TestCases()
		{
			yield return new object[] { "input", "INPUT" };
			yield return new object[] { "inPut", "INPUT" };
			yield return new object[] { "INpUT", "INPUT" };
			yield return new object[] { "in-put", "IN-PUT" };
			yield return new object[] { "in put", "IN PUT" };
			yield return new object[] { "in@put", "IN@PUT" };
			yield return new object[] { "in_put", "IN_PUT" };
		}

		[Test]
		[TestCaseSource(nameof(TestCases))]
		public void ShouldNormalizeName(string inout, string expected)
		{
			IServiceCollection services = new ServiceCollection();
			ServiceCollectionExtensions.AddPermissionsAuthorization(services);
			ServiceCollectionExtensions.AddClaimsProvider<TestClaimsProvider>(services);
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer service = serviceProvider.GetRequiredService<MadEyeMatt.AspNetCore.Authorization.Permissions.Abstractions.IPermissionLookupNormalizer>();
			string result = service.NormalizeName(inout);

			result.Should().NotBeNullOrEmpty().And.Be(expected);
		}
	}
}
