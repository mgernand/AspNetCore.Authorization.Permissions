﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SamplePermissions;

#nullable disable

namespace SamplePermissions.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("identity")
                .HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityRolePermission<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PermissionId")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions", "identity");

                    b.HasData(
                        new
                        {
                            RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483",
                            PermissionId = "5b9c4926-3dc6-447c-a092-addab890a15f"
                        },
                        new
                        {
                            RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
                            PermissionId = "5b9c4926-3dc6-447c-a092-addab890a15f"
                        },
                        new
                        {
                            RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
                            PermissionId = "e123b8c0-0646-4075-b73e-07ca9d611c8e"
                        },
                        new
                        {
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            PermissionId = "5b9c4926-3dc6-447c-a092-addab890a15f"
                        },
                        new
                        {
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            PermissionId = "be5b92e5-c6c6-480b-b235-d4df402a73cc"
                        },
                        new
                        {
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            PermissionId = "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2"
                        },
                        new
                        {
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            PermissionId = "ef54d62d-a36b-4ab3-b868-f170c0054fac"
                        });
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityTenantRole<string>", b =>
                {
                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("TenantId", "RoleId");

                    b.ToTable("TenantRoles", "identity");
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityPermission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("PermissionNameIndex");

                    b.ToTable("Permissions", "identity");

                    b.HasData(
                        new
                        {
                            Id = "5b9c4926-3dc6-447c-a092-addab890a15f",
                            ConcurrencyStamp = "bbc91bd8-677b-43e4-a4b9-b9c8042800d2",
                            Name = "Invoice.Read",
                            NormalizedName = "INVOICE.READ"
                        },
                        new
                        {
                            Id = "be5b92e5-c6c6-480b-b235-d4df402a73cc",
                            ConcurrencyStamp = "5156f402-22a8-4223-937f-8103fc673da9",
                            Name = "Invoice.Write",
                            NormalizedName = "INVOICE.WRITE"
                        },
                        new
                        {
                            Id = "e123b8c0-0646-4075-b73e-07ca9d611c8e",
                            ConcurrencyStamp = "26142354-be5d-4f5d-adfc-ddd582295ff6",
                            Name = "Invoice.Delete",
                            NormalizedName = "INVOICE.DELETE"
                        },
                        new
                        {
                            Id = "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2",
                            ConcurrencyStamp = "26a64c80-f452-411d-9eda-870aa10beee8",
                            Name = "Invoice.Send",
                            NormalizedName = "INVOICE.SEND"
                        },
                        new
                        {
                            Id = "ef54d62d-a36b-4ab3-b868-f170c0054fac",
                            ConcurrencyStamp = "9a48a37d-4973-49ef-a4dd-3c54a7988cd3",
                            Name = "Invoice.Payment",
                            NormalizedName = "INVOICE.PAYMENT"
                        });
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", "identity");

                    b.HasData(
                        new
                        {
                            Id = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483",
                            ConcurrencyStamp = "68094f47-6d38-4428-bc98-2225144bfb74",
                            Name = "Boss",
                            NormalizedName = "BOSS"
                        },
                        new
                        {
                            Id = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
                            ConcurrencyStamp = "f51c6d1c-e043-4493-8b3a-1b67340795e8",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            ConcurrencyStamp = "b19c5abe-c633-42b0-870e-d4f5f026a3e9",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        });
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityTenant", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("DatabaseName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasSeparateDatabase")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHierarchical")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("TenantNameIndex");

                    b.ToTable("AspNetTenants", "identity");
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityTenant<string>", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("DatabaseName")
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<bool>("HasSeparateDatabase")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsHierarchical")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Tenants", "identity");
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasMaxLength(256)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("Users", "identity");

                    b.HasData(
                        new
                        {
                            Id = "a0f112af-5e39-4b3f-bc50-015591861ec0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "96e8bbbd-62f9-490e-810a-6d722d7bda02",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bf21e5cf-1f5a-40b9-86e0-534dff3af2b2",
                            TwoFactorEnabled = false,
                            UserName = "boss@company"
                        },
                        new
                        {
                            Id = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e23a4ce4-85d2-4db1-82c3-3624b0ea2534",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "bcf4d471-98c7-4181-a018-b8ddb9a5332d",
                            TwoFactorEnabled = false,
                            UserName = "manager@company"
                        },
                        new
                        {
                            Id = "04517a45-d6f5-4993-888b-04c924902b3a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a4fbef50-ab25-4829-b97a-365a10705d86",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "04017357-dd57-48b4-9d0f-b2f0b36ea874",
                            TwoFactorEnabled = false,
                            UserName = "employee@company"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", "identity");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", "identity");

                    b.HasData(
                        new
                        {
                            UserId = "a0f112af-5e39-4b3f-bc50-015591861ec0",
                            RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
                        },
                        new
                        {
                            UserId = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
                            RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
                        },
                        new
                        {
                            UserId = "04517a45-d6f5-4993-888b-04c924902b3a",
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "identity");
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityRolePermission<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityPermission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityUser", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityTenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.PermissionsIdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
