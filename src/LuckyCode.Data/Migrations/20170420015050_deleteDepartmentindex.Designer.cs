using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyCode.Data.Migrations
{
    [DbContext(typeof(LiteCodeContext))]
    [Migration("20170420015050_deleteDepartmentindex")]
    partial class deleteDepartmentindex
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("LiteCode.Entity.OauthBase.SysDepartment", b =>
                {
                    b.Property<string>("DepartmentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("DepartmentId")
                        .HasMaxLength(50);

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasColumnName("DepartmentName")
                        .HasMaxLength(50);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnName("Description")
                        .HasMaxLength(500);

                    b.Property<int>("DistributorId");

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnName("ParentId")
                        .HasMaxLength(50);

                    b.Property<int>("Sort")
                        .HasColumnName("Sort");

                    b.Property<int>("State")
                        .HasColumnName("State");

                    b.HasKey("DepartmentId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("LiteCode.Entity.OauthBase.SysModules", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ActionName");

                    b.Property<string>("ApplicationId");

                    b.Property<string>("AreaName");

                    b.Property<string>("ControllerName");

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("Icon");

                    b.Property<bool>("IsDelete");

                    b.Property<bool>("IsExpand");

                    b.Property<bool>("IsValidPurView");

                    b.Property<string>("ModuleDescription");

                    b.Property<short>("ModuleLayer");

                    b.Property<string>("ModuleName");

                    b.Property<int>("ModuleType");

                    b.Property<string>("ParentId");

                    b.Property<int>("PurviewNum");

                    b.Property<long>("PurviewSum");

                    b.Property<int>("Sort");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.ToTable("Sys_Modules");
                });

            modelBuilder.Entity("LiteCode.Entity.OauthBase.SysRoleModules", b =>
                {
                    b.Property<string>("ModuleId");

                    b.Property<string>("RoleId");

                    b.Property<string>("ApplicationId");

                    b.Property<string>("ControllerName");

                    b.Property<long>("PurviewSum");

                    b.HasKey("ModuleId", "RoleId");

                    b.ToTable("Sys_RoleModules");
                });

            modelBuilder.Entity("LiteCode.Entity.SysApplication", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(128);

                    b.Property<string>("ApplicationName")
                        .IsRequired()
                        .HasColumnName("ApplicationName")
                        .HasColumnType("nvarchar(256)");

                    b.Property<string>("ApplicationUrl")
                        .HasColumnName("ApplicationUrl")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.HasKey("Id")
                        .HasName("Id");

                    b.ToTable("Sys_Application");
                });

            modelBuilder.Entity("LiteCode.Entity.SysRoles", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateTime");

                    b.Property<bool>("IsAllowDelete");

                    b.Property<bool>("IsDelete");

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.Property<string>("RoleDescription");

                    b.Property<string>("RoleName");

                    b.Property<int>("RoleType");

                    b.Property<int>("Sort");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("Sys_Roles");
                });

            modelBuilder.Entity("LiteCode.Entity.SysUsers", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreateTime");

                    b.Property<string>("DepartmentId");

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("IsLock");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Sys_Users");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("LiteCode.Entity.OauthBase.SysModules", b =>
                {
                    b.HasOne("LiteCode.Entity.SysApplication", "Application")
                        .WithMany("Moduleses")
                        .HasForeignKey("ApplicationId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("LiteCode.Entity.SysRoles")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("LiteCode.Entity.SysUsers")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("LiteCode.Entity.SysUsers")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<string>", b =>
                {
                    b.HasOne("LiteCode.Entity.SysRoles")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("LiteCode.Entity.SysUsers")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
