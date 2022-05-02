﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SampleTenant;

#nullable disable

namespace SampleTenant.Migrations
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

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityPermission", b =>
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
                            ConcurrencyStamp = "37516013-e2e7-44eb-98b3-d9929c367a55",
                            Name = "Invoice.Read",
                            NormalizedName = "INVOICE.READ"
                        },
                        new
                        {
                            Id = "be5b92e5-c6c6-480b-b235-d4df402a73cc",
                            ConcurrencyStamp = "01cef358-e215-4a0a-be5d-56b30821dbf5",
                            Name = "Invoice.Write",
                            NormalizedName = "INVOICE.WRITE"
                        },
                        new
                        {
                            Id = "e123b8c0-0646-4075-b73e-07ca9d611c8e",
                            ConcurrencyStamp = "bdb2dd41-46ce-4550-b86a-e841132556d3",
                            Name = "Invoice.Delete",
                            NormalizedName = "INVOICE.DELETE"
                        },
                        new
                        {
                            Id = "9dcb49c9-e732-4fb9-80a1-2c5efda61ab2",
                            ConcurrencyStamp = "5bd71f26-3936-42f2-8743-c98675ecce86",
                            Name = "Invoice.Send",
                            NormalizedName = "INVOICE.SEND"
                        },
                        new
                        {
                            Id = "ef54d62d-a36b-4ab3-b868-f170c0054fac",
                            ConcurrencyStamp = "fdee585a-3a99-40e3-8540-5d2b60c37503",
                            Name = "Invoice.Payment",
                            NormalizedName = "INVOICE.PAYMENT"
                        },
                        new
                        {
                            Id = "9c8dd197-bc4e-42b2-8789-f0b4481a05ed",
                            ConcurrencyStamp = "ce2af0f2-28e9-47d9-afba-e7916b1b7148",
                            Name = "Invoice.Statistics",
                            NormalizedName = "INVOICE.STATISTICS"
                        },
                        new
                        {
                            Id = "f1af54df-c9e7-4570-850f-c563732c15b4",
                            ConcurrencyStamp = "4f8d2531-e29c-4852-b326-47ca1c61a769",
                            Name = "Invoice.TaxExport",
                            NormalizedName = "INVOICE.TAXEXPORT"
                        });
                });

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

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityTenant", b =>
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

                    b.ToTable("Tenants", "identity");

                    b.HasData(
                        new
                        {
                            Id = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            ConcurrencyStamp = "8caaa6bf-ac42-4b8d-83cd-46e7c2b8bc78",
                            DisplayName = "Startup LLC.",
                            HasSeparateDatabase = false,
                            IsHierarchical = false,
                            Name = "Startup",
                            NormalizedName = "STARTUP"
                        },
                        new
                        {
                            Id = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            ConcurrencyStamp = "6d48e326-e73b-4483-ae12-470b6fbc344b",
                            DisplayName = "Company Inc.",
                            HasSeparateDatabase = false,
                            IsHierarchical = false,
                            Name = "Company",
                            NormalizedName = "COMPANY"
                        },
                        new
                        {
                            Id = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            ConcurrencyStamp = "c8c795e9-23f0-40c2-ba07-820379b127a9",
                            DisplayName = "Corporate Corp.",
                            HasSeparateDatabase = false,
                            IsHierarchical = false,
                            Name = "Corporate",
                            NormalizedName = "CORPORATE"
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

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityTenantUser", b =>
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
                            Id = "ea346013-ec20-4a69-8a60-8684ffb58a5f",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "e5af6727-4c38-484b-ab95-ea1301e9f6ff",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@STARTUP",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "4170e43f-c60d-4800-b221-c273752130b9",
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            TwoFactorEnabled = false,
                            UserName = "boss@startup"
                        },
                        new
                        {
                            Id = "50cd8ad5-b945-4541-90c9-156f6940c18b",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "7f6d22ee-177f-4e89-ad3e-cfd101350b2b",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@STARTUP",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "6e1d1b2a-5341-43d7-9186-74bb3e55357f",
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            TwoFactorEnabled = false,
                            UserName = "manager@startup"
                        },
                        new
                        {
                            Id = "142838fe-7e64-484b-a769-87b327726715",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "82ea7124-c97a-422d-bb43-5305ebf2983e",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@STARTUP",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7247a853-1547-4a6e-be87-93030b558fb8",
                            TenantId = "7d706acd-f5fd-4979-9e3f-c77a0bd596b2",
                            TwoFactorEnabled = false,
                            UserName = "employee@startup"
                        },
                        new
                        {
                            Id = "a0f112af-5e39-4b3f-bc50-015591861ec0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "676529e3-62a8-4be8-b994-fd3fe340dc04",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "d68b3bdf-38a1-46f8-b191-e7c1e47d8675",
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            TwoFactorEnabled = false,
                            UserName = "boss@company"
                        },
                        new
                        {
                            Id = "90a4dd66-78d1-4fff-a507-7f88735f7ab6",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "a42ac0c5-3cfd-4f87-9c98-37c1966eabac",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "257ccb73-9a82-4f29-a649-952031465d15",
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            TwoFactorEnabled = false,
                            UserName = "manager@company"
                        },
                        new
                        {
                            Id = "04517a45-d6f5-4993-888b-04c924902b3a",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4124f7a0-2ff3-4162-8bb4-12255a285a85",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@COMPANY",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "44bea655-7ef9-48b9-8064-2dc42848b7a0",
                            TenantId = "ee5128d3-4cad-4bcc-aa64-f6abbb30da46",
                            TwoFactorEnabled = false,
                            UserName = "employee@company"
                        },
                        new
                        {
                            Id = "dbcf2449-14b7-4766-9829-ae65604500b0",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "defd841f-9f53-4c0a-ae88-2ceadde41832",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "BOSS@CORPORATE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "e11d7911-9914-4acc-86c1-829cdfd164d8",
                            TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            TwoFactorEnabled = false,
                            UserName = "boss@corporate"
                        },
                        new
                        {
                            Id = "aeb83173-9ba7-4aa2-ab82-e434e2dcbe55",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "4b3f7ed1-fb53-405a-9265-e31c2c341232",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "MANAGER@CORPORATE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "a48e932a-0c43-4e7e-b779-aef4884687f0",
                            TenantId = "49a049d2-23ad-41df-8806-240aebaa2f17",
                            TwoFactorEnabled = false,
                            UserName = "manager@corporate"
                        },
                        new
                        {
                            Id = "e420f504-d953-4bec-95fd-1613fd760652",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "6ccfdbe2-a192-4bd9-891d-418f26d8db31",
                            EmailConfirmed = false,
                            LockoutEnabled = false,
                            NormalizedUserName = "EMPLOYEE@CORPORATE",
                            PasswordHash = "AQAAAAEAACcQAAAAEJ5tM19BCnMGTsQz8r8yFNvc4q9iWwkmCYHCsQYQUjlJ3XbZr1fx3tEC1QNNFxiuKA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "98564b9b-6756-4bef-b2e9-52280440d41b",
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
                            ConcurrencyStamp = "1fec374c-1f4d-4162-b87d-2ea2bbdfef9f",
                            Name = "Boss",
                            NormalizedName = "BOSS"
                        },
                        new
                        {
                            Id = "2c77ea15-1559-4b9b-bc20-1d64892e4297",
                            ConcurrencyStamp = "88c5a136-0d2f-48c0-a889-0d64a0bad28b",
                            Name = "Manager",
                            NormalizedName = "MANAGER"
                        },
                        new
                        {
                            Id = "c7ebaa11-c7ed-4357-b287-e0f2dd1eb3f2",
                            ConcurrencyStamp = "cc16fe70-a781-4785-8c23-475e81d8b596",
                            Name = "Employee",
                            NormalizedName = "EMPLOYEE"
                        },
                        new
                        {
                            Id = "ecae3c35-0d88-424f-a1bc-31cba5add7a7",
                            ConcurrencyStamp = "db0f50f0-04fc-4984-b859-51a617574048",
                            Name = "Free",
                            NormalizedName = "FREE"
                        },
                        new
                        {
                            Id = "49161cff-c451-4c44-ac59-467883fe1517",
                            ConcurrencyStamp = "d3209e44-f609-4561-a252-015d3078d728",
                            Name = "Basic",
                            NormalizedName = "BASIC"
                        },
                        new
                        {
                            Id = "c7602fdc-a7ef-4c6c-a69f-f8d2dbb5d230",
                            ConcurrencyStamp = "02b62435-6c6a-4ae2-affd-8ab079ca6096",
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
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("UserTokens", "identity");
                });

            modelBuilder.Entity("SampleTenant.Model.Invoice", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Note")
                        .HasColumnType("TEXT");

                    b.Property<string>("TenantId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("Total")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Invoices", "identity");
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityRolePermission<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.IdentityPermission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AspNetCore.Authorization.Permissions.Identity.IdentityTenantUser", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.IdentityTenant", null)
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
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.IdentityTenantUser", null)
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

                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("AspNetCore.Authorization.Permissions.Identity.IdentityTenantUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
