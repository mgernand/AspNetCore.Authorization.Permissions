namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System;
	using System.Collections.Generic;
	using System.Security.Claims;
	using System.Threading.Tasks;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class UpperInvariantPermissionLookupNormalizerTests
	{
		private class TestClaimsProvider : IClaimsProvider
		{
			/// <inheritdoc />
			public async Task<IReadOnlyCollection<Claim>> GetPermissionClaimsForUserAsync(string userId)
			{
				return Array.Empty<Claim>();
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
			services.AddPermissionsAuthorization();
			services.AddClaimsProvider<TestClaimsProvider>();
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IPermissionLookupNormalizer service = serviceProvider.GetRequiredService<IPermissionLookupNormalizer>();
			string result = service.NormalizeName(inout);

			result.Should().NotBeNullOrEmpty().And.Be(expected);
		}
	}
}
