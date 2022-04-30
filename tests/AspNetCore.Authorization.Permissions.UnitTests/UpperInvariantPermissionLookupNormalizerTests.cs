namespace AspNetCore.Authorization.Permissions.UnitTests
{
	using System.Collections.Generic;
	using AspNetCore.Authorization.Permissions.Abstractions;
	using FluentAssertions;
	using Microsoft.Extensions.DependencyInjection;
	using NUnit.Framework;

	[TestFixture]
	public class UpperInvariantPermissionLookupNormalizerTests
	{
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
			services.AddPermissions();
			ServiceProvider serviceProvider = services.BuildServiceProvider();

			IPermissionLookupNormalizer service = serviceProvider.GetRequiredService<IPermissionLookupNormalizer>();
			string result = service.NormalizeName(inout);

			result.Should().NotBeNullOrEmpty().And.Be(expected);
		}
	}
}
