﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleTenant;

#nullable disable

namespace SampleTenant.Migrations
{
    [DbContext(typeof(InvoicesContext))]
    partial class InvoicesContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.4");

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityPermission", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("PermissionNameIndex");

                    b.ToTable("Permissions", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "5b9c4926-3dc6-447c-a092-addab890a15f",
                            ConcurrencyStamp = "ed978ab6-ac7a-47ce-a670-fb776156ff5c",
                            Name = "Invoice.Read",
                            NormalizedName = "INVOICE.READ"
                        },
                        new
                        {
                            Id = "be5b92e5-c6c6-480b-b235-d4df402a73cc",
                            ConcurrencyStamp = "90326547-5bcd-4ffd-9666-be3e12e63621",
                            Name = "Invoice.Write",
                            NormalizedName = "INVOICE.WRITE"
                        },
                        new
                        {
                            Id = "e123b8c0-0646-4075-b73e-07ca9d611c8e",
                            ConcurrencyStamp = "03330399-aabb-4fba-b4d3-8252dc84311d",
                            Name = "Invoice.Delete",
                            NormalizedName = "INVOICE.DELETE"
                        },
                        new
                        {
                            Id = "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2",
                            ConcurrencyStamp = "7e3fdc41-1def-42d8-b698-22d8f67ca92e",
                            Name = "Invoice.Send",
                            NormalizedName = "INVOICE.SEND"
                        },
                        new
                        {
                            Id = "ef54d62d-a36b-4ab3-b868-f170c0054fac",
                            ConcurrencyStamp = "b189ffd2-202a-41d6-917e-69d5d46439af",
                            Name = "Invoice.Payment",
                            NormalizedName = "INVOICE.PAYMENT"
                        },
                        new
                        {
                            Id = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed",
                            ConcurrencyStamp = "0d92f40d-b529-4629-8a39-5abfcf55b1cb",
                            Name = "Invoice.Statistics",
                            NormalizedName = "INVOICE.STATISTICS"
                        },
                        new
                        {
                            Id = "f1af54df-c9e7-4570-850f-c563732c15b4",
                            ConcurrencyStamp = "5f2c2bbe-e1c8-4b9d-b5e4-06b2e34c0a66",
                            Name = "Invoice.TaxExport",
                            NormalizedName = "INVOICE.TAXEXPORT"
                        });
                });

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityRolePermission<string>", b =>
                {
                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("PermissionId")
                        .HasColumnType("TEXT");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RolePermissions", (string)null);

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
                        },
                        new
                        {
                            RoleId = "49161cff-c451-4c44-ac59-467883fe1517",
                            PermissionId = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed"
                        },
                        new
                        {
                            RoleId = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
                            PermissionId = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed"
                        },
                        new
                        {
                            RoleId = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
                            PermissionId = "f1af54df-c9e7-4570-850f-c563732c15b4"
                        });
                });

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenant", b =>
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
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("TenantNameIndex");

                    b.ToTable("Tenants", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            ConcurrencyStamp = "99429101-7bd8-4b6b-83b0-295d9a8e068e",
                            DisplayName = "Startup LLC.",
                            HasSeparateDatabase = false,
                            IsHierarchical = false,
                            Name = "Startup",
                            NormalizedName = "STARTUP"
                        },
                        new
                        {
                            Id = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            ConcurrencyStamp = "dc492669-04d9-466b-9b8c-8d69607ee2e3",
                            DisplayName = "Company Inc.",
                            HasSeparateDatabase = false,
                            IsHierarchical = false,
                            Name = "Company",
                            NormalizedName = "COMPANY"
                        },
                        new
                        {
                            Id = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            ConcurrencyStamp = "6ae7c9e8-a348-4602-9b1a-6f760dc5a0d1",
                            DisplayName = "Corporate Corp.",
                            HasSeparateDatabase = false,
                            IsHierarchical = false,
                            Name = "Corporate",
                            NormalizedName = "CORPORATE"
                        });
                });

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantRole<string>", b =>
                {
                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("TenantId", "RoleId");

                    b.ToTable("TenantRoles", (string)null);

                    b.HasData(
                        new
                        {
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            RoleId = "ecae3c35-0d88-424f-a1bc-31cba5add7a7"
                        },
                        new
                        {
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            RoleId = "49161cff-c451-4c44-ac59-467883fe1517"
                        },
                        new
                        {
                            TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            RoleId = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230"
                        });
                });

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasMaxLength(128)
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
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasDatabaseName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasDatabaseName("UserNameIndex");

                    b.HasIndex("TenantId");

                    b.ToTable("Users", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "ea346013-ec20-4a69-8a60-8684ffb58a5f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "b8ba7187-db51-434d-b1c2-2571f5002884",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@STARTUP",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "fd5d017f-8085-4b0a-bd07-203f75219711",
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            TwoFactorEnabled = false,
                            UserName = "boss@startup"
                        },
                        new
                        {
                            Id = "50cd8ad5-b945-4541-90c9-156f6940c18b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "aabcac7e-9951-479e-806e-cf6329c4ef55",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@STARTUP",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "cbc6ce1b-1e42-4b2a-b3ca-692c2b021c24",
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            TwoFactorEnabled = false,
                            UserName = "manager@startup"
                        },
                        new
                        {
                            Id = "142838fe-7e64-484b-a769-87b327726715",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "d113f27d-e5f2-4d25-9133-a3043e0cbc86",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@STARTUP",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "1c3c1fb7-65e6-4538-845b-41553afb5b87",
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            TwoFactorEnabled = false,
                            UserName = "employee@startup"
                        },
                        new
                        {
                            Id = "a0f112af-5e39-4b3f-bc50-015591861ec0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "1990a8c7-6ad8-44bc-b683-1ba4ba6bf4e5",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "37728c18-84be-4667-b5bd-7c3577e61513",
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            TwoFactorEnabled = false,
                            UserName = "boss@company"
                        },
                        new
                        {
                            Id = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "f6e97f88-e3cc-4a86-bacb-87d1b9015453",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "198439ee-4290-48ae-b7f6-48354abd690b",
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            TwoFactorEnabled = false,
                            UserName = "manager@company"
                        },
                        new
                        {
                            Id = "04517a45-d6f5-4993-888b-04c924902b3a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "3cdf0e34-abef-43b2-a7ed-6cd6da89f1ab",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "64891915-5d85-4f74-8e4a-c7e7f072a0d8",
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            TwoFactorEnabled = false,
                            UserName = "employee@company"
                        },
                        new
                        {
                            Id = "dbcf2449-14b7-4766-9829-ae65604500b0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "bb0d464b-ec5b-4623-ae81-737fc7577723",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@CORPORATE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "8ae11f84-0b7f-421b-9cbc-000eda90b58f",
                            TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            TwoFactorEnabled = false,
                            UserName = "boss@corporate"
                        },
                        new
                        {
                            Id = "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "585336bf-dee2-4b2a-af1e-fe7387472653",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@CORPORATE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4e6fff6a-e2ae-4d59-a6c4-fb30e295ead6",
                            TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            TwoFactorEnabled = false,
                            UserName = "manager@corporate"
                        },
                        new
                        {
                            Id = "e420f504-d953-4bec-95fd-1613fd760652",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "2a3d32c4-037a-4a34-9e5a-599451c515a2",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@CORPORATE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "394c97ba-d9ab-4c5a-bdf2-2af55b9be190",
                            TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            TwoFactorEnabled = false,
                            UserName = "employee@corporate"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasDatabaseName("RoleNameIndex");

                    b.ToTable("Roles", (string)null);

                    b.HasData(
                        new
                        {
                            Id = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483",
                            Name = "Boss",
                            NormalizedName = "BOSS"
                        },
                        new
                        {
                            Id = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "ecae3c35-0d88-424f-a1bc-31cba5add7a7",
                            Name = "Free",
                            NormalizedName = "FREE"
                        },
                        new
                        {
                            Id = "49161cff-c451-4c44-ac59-467883fe1517",
                            Name = "Basic",
                            NormalizedName = "BASIC"
                        },
                        new
                        {
                            Id = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
                            Name = "Professional",
                            NormalizedName = "PROFESSIONAL"
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

                    b.ToTable("RoleClaims", (string)null);
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

                    b.ToTable("UserClaims", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderKey")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins", (string)null);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserRoles", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = "ea346013-ec20-4a69-8a60-8684ffb58a5f",
                            RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
                        },
                        new
                        {
                            UserId = "a0f112af-5e39-4b3f-bc50-015591861ec0",
                            RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
                        },
                        new
                        {
                            UserId = "dbcf2449-14b7-4766-9829-ae65604500b0",
                            RoleId = "b0df7eae-a4f9-4d58-8795-ead2aaf6a483"
                        },
                        new
                        {
                            UserId = "50cd8ad5-b945-4541-90c9-156f6940c18b",
                            RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
                        },
                        new
                        {
                            UserId = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
                            RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
                        },
                        new
                        {
                            UserId = "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55",
                            RoleId = "2c77ea15-1559-4b9b-bc20-1d64892e4297"
                        },
                        new
                        {
                            UserId = "142838fe-7e64-484b-a769-87b327726715",
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
                        },
                        new
                        {
                            UserId = "04517a45-d6f5-4993-888b-04c924902b3a",
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
                        },
                        new
                        {
                            UserId = "e420f504-d953-4bec-95fd-1613fd760652",
                            RoleId = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasMaxLength(128)
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", (string)null);
                });

            modelBuilder.Entity("SampleTenant.Model.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantID")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("TenantID")
                        .HasDatabaseName("InvoiceTenantIdIndex");

                    b.ToTable("Invoices", (string)null);

                    b.HasData(
                        new
                        {
                            Id = new Guid("8ceef93c-1bf8-4bd0-9556-7f6b0886f7db"),
                            Note = "This is a Startup invoice.",
                            TenantID = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            Total = 99.95m
                        },
                        new
                        {
                            Id = new Guid("ab41432a-b190-4ee8-8923-747950ca534f"),
                            Note = "This is a Startup invoice.",
                            TenantID = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            Total = 99.95m
                        },
                        new
                        {
                            Id = new Guid("61a3c165-df02-445f-b4bf-d3a6d6f581d2"),
                            Note = "This is a Startup invoice.",
                            TenantID = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            Total = 99.95m
                        },
                        new
                        {
                            Id = new Guid("d686039e-a9de-4ec0-b6a1-40ded147636d"),
                            Note = "This is a Company invoice.",
                            TenantID = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            Total = 199.95m
                        },
                        new
                        {
                            Id = new Guid("f9598106-a510-4d31-957c-005b6e2c1892"),
                            Note = "This is a Company invoice.",
                            TenantID = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            Total = 199.95m
                        },
                        new
                        {
                            Id = new Guid("1190e6e7-75d4-4336-a168-e42b063e7485"),
                            Note = "This is a Company invoice.",
                            TenantID = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            Total = 199.95m
                        },
                        new
                        {
                            Id = new Guid("753ffd62-a368-439d-9aa2-19f47fcb9e28"),
                            Note = "This is a Corporate invoice.",
                            TenantID = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            Total = 399.95m
                        },
                        new
                        {
                            Id = new Guid("a05d5bd8-c6a8-4286-a771-4311474742b8"),
                            Note = "This is a Corporate invoice.",
                            TenantID = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            Total = 399.95m
                        },
                        new
                        {
                            Id = new Guid("6e6cdffa-55ef-46cc-8020-c678feef6499"),
                            Note = "This is a Corporate invoice.",
                            TenantID = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            Total = 399.95m
                        });
                });

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityRolePermission<string>", b =>
                {
                    b.HasOne("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityPermission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantUser", b =>
                {
                    b.HasOne("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenant", null)
                        .WithMany()
                        .HasForeignKey("TenantId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MadEyeMatt.AspNetCore.Identity.Permissions.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
