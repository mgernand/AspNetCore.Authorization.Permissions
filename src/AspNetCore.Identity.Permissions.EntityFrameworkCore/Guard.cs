namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore
{
	using System;
	using System.Diagnostics.CodeAnalysis;
	using System.Runtime.CompilerServices;

	internal static class Guard
	{
		internal static void ThrowIfNull([NotNull] object argument, [CallerArgumentExpression("argument")] string parameterName = null)
		{
			if (argument != null)
			{
				return;
			}

			throw new ArgumentNullException(parameterName);
		}

		public static void ThrowIfNullOrWhiteSpace([NotNull] string argument, [CallerArgumentExpression("argument")] string parameterName = null)
		{
			if (!string.IsNullOrWhiteSpace(argument))
			{
				return;
			}

			throw new ArgumentException(argument, parameterName);
		}
	}
}