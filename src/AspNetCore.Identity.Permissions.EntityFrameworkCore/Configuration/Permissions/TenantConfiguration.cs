namespace MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Configuration.Permissions
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Reflection;
	using JetBrains.Annotations;
	using MadEyeMatt.AspNetCore.Identity.Permissions.EntityFrameworkCore.Properties;
	using Microsoft.AspNetCore.Identity;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Metadata.Builders;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TTenant"></typeparam>
	[PublicAPI]
	public class TenantConfiguration<TTenant> : TenantConfiguration<TTenant, string>
		where TTenant : IdentityTenant<string>
	{
	}

	/// <summary>
	///     An entity type configuration.
	/// </summary>
	/// <typeparam name="TTenant"></typeparam>
	/// <typeparam name="TKey"></typeparam>
	[PublicAPI]
	public class TenantConfiguration<TTenant, TKey> : IEntityTypeConfiguration<TTenant>
		where TTenant : IdentityTenant<TKey>
		where TKey : IEquatable<TKey>
	{
		/// <summary>
		///     Gets or sets the table name.
		/// </summary>
		public string Table { get; init; } = "AspNetTenants";

		/// <summary>
		///     If set, all properties on type <typeparamref name="TTenant" /> marked with a
		///     <see cref="ProtectedPersonalDataAttribute" /> will be converted using this <see cref="ValueConverter" />.
		/// </summary>
		public ValueConverter<string, string> PersonalDataConverter { get; set; }

		/// <inheritdoc />
		public virtual void Configure(EntityTypeBuilder<TTenant> builder)
		{
			builder.ToTable(this.Table);

			builder.HasKey(x => x.Id);
			builder.HasIndex(x => x.NormalizedName).HasDatabaseName("TenantNameIndex").IsUnique();

			builder.Property(x => x.ConcurrencyStamp).IsConcurrencyToken();
			builder.Property(x => x.Name).HasMaxLength(256);
			builder.Property(x => x.NormalizedName).HasMaxLength(256);

			if(this.PersonalDataConverter != null)
			{
				IEnumerable<PropertyInfo> personalDataProps = typeof(TTenant)
					.GetProperties()
					.Where(prop => Attribute.IsDefined(prop, typeof(ProtectedPersonalDataAttribute)));

				foreach(PropertyInfo p in personalDataProps)
				{
					if(p.PropertyType != typeof(string))
					{
						throw new InvalidOperationException(Resources.CanOnlyProtectStrings);
					}

					builder.Property(typeof(string), p.Name).HasConversion(this.PersonalDataConverter);
				}
			}
		}
	}
}
