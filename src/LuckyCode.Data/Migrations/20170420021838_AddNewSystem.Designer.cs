using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyCode.Data.Migrations
{
    [DbContext(typeof(LiteCodeContext))]
    [Migration("20170420021838_AddNewSystem")]
    partial class AddNewSystem
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1");

            modelBuilder.Entity("LiteCode.Entity.News.Category", b =>
                {
                    b.Property<string>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("CategoryID")
                        .HasMaxLength(30);

                    b.Property<string>("CategoryType")
                        .HasColumnName("CategoryType")
                        .HasMaxLength(50);

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate");

                    b.Property<string>("Description")
                        .HasColumnName("Description")
                        .HasMaxLength(50);

                    b.Property<int>("DisplayOrder")
                        .HasColumnName("DisplayOrder");

                    b.Property<string>("HyperLink")
                        .HasColumnName("HyperLink")
                        .HasMaxLength(250);

                    b.Property<string>("ParentId")
                        .IsRequired()
                        .HasColumnName("ParentID")
                        .HasMaxLength(30);

                    b.Property<string>("SortCode")
                        .HasColumnName("SortCode")
                        .HasMaxLength(10);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasMaxLength(150);

                    b.HasKey("CategoryId");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("LiteCode.Entity.News.Link", b =>
                {
                    b.Property<Guid>("LinkID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("LinkID");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate");

                    b.Property<int>("DisplayOrder")
                        .HasColumnName("DisplayOrder");

                    b.Property<string>("ImageUrl")
                        .HasColumnName("ImageUrl")
                        .HasMaxLength(150);

                    b.Property<bool>("IsImage")
                        .HasColumnName("IsImage");

                    b.Property<bool>("IsLock")
                        .HasColumnName("IsLock");

                    b.Property<string>("Title")
                        .HasColumnName("Title")
                        .HasMaxLength(150);

                    b.Property<string>("UserEmail")
                        .HasColumnName("UserEmail")
                        .HasMaxLength(150);

                    b.Property<string>("UserName")
                        .HasColumnName("UserName")
                        .HasMaxLength(50);

                    b.Property<string>("UserTel")
                        .HasColumnName("UserTel")
                        .HasMaxLength(50);

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasColumnName("WebUrl")
                        .HasMaxLength(150);

                    b.HasKey("LinkID");

                    b.ToTable("Links");
                });

            modelBuilder.Entity("LiteCode.Entity.News.NewsArticle", b =>
                {
                    b.Property<Guid>("ArticleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ArticleID");

                    b.Property<string>("Author")
                        .HasColumnName("Author")
                        .HasMaxLength(50);

                    b.Property<string>("CategoryID")
                        .IsRequired()
                        .HasColumnName("CategoryID")
                        .HasMaxLength(30);

                    b.Property<int>("ClickNum")
                        .HasColumnName("ClickNum");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnName("CreateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Editor")
                        .HasColumnName("Editor")
                        .HasMaxLength(50);

                    b.Property<string>("ImgUrl")
                        .HasColumnName("ImgUrl")
                        .HasMaxLength(250);

                    b.Property<bool>("IsCommend")
                        .HasColumnName("IsCommend");

                    b.Property<bool>("IsComment")
                        .HasColumnName("IsComment");

                    b.Property<bool>("IsHot")
                        .HasColumnName("IsHot");

                    b.Property<bool>("IsImage")
                        .HasColumnName("IsImage");

                    b.Property<bool>("IsLock")
                        .HasColumnName("IsLock");

                    b.Property<bool>("IsSlide")
                        .HasColumnName("IsSlide");

                    b.Property<bool>("IsTop")
                        .HasColumnName("IsTop");

                    b.Property<string>("KeyWord")
                        .HasColumnName("KeyWord")
                        .HasMaxLength(150);

                    b.Property<int?>("OrgID")
                        .HasColumnName("OrgID");

                    b.Property<string>("Source")
                        .HasColumnName("Source")
                        .HasMaxLength(150);

                    b.Property<string>("Summarize")
                        .HasColumnName("Summarize")
                        .HasMaxLength(500);

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasMaxLength(250);

                    b.Property<DateTime>("UpdateDate")
                        .HasColumnName("UpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("UserID")
                        .HasColumnName("UserID")
                        .HasMaxLength(128);

                    b.HasKey("ArticleID");

                    b.HasIndex("CategoryID");

                    b.ToTable("NewsArticles");
                });

            modelBuilder.Entity("LiteCode.Entity.News.NewsArticleText", b =>
                {
                    b.Property<long>("ArticleTextID")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ArticleTextID");

                    b.Property<Guid>("ArticleID")
                        .HasColumnName("ArticleID");

                    b.Property<string>("ArticleText")
                        .HasColumnName("ArticleText")
                        .HasColumnType("text");

                    b.Property<string>("NoHtml")
                        .HasColumnName("NoHtml")
                        .HasColumnType("text");

                    b.HasKey("ArticleTextID");

                    b.HasIndex("ArticleID");

                    b.ToTable("NewsArticleText");
                });

            modelBuilder.Entity("LiteCode.Entity.News.NewsBanner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateTime")
                        .HasColumnName("CreateTime");

                    b.Property<string>("ImageUrl")
                        .HasColumnName("ImageUrl")
                        .HasMaxLength(256);

                    b.Property<bool>("IsDeleted")
                        .HasColumnName("IsDeleted");

                    b.Property<int>("Sort")
                        .HasColumnName("Sort");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnName("Title")
                        .HasMaxLength(128);

                    b.Property<string>("Url")
                        .HasColumnName("Url")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("NewsBanner");
                });

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

            modelBuilder.Entity("LiteCode.Entity.News.NewsArticle", b =>
                {
                    b.HasOne("LiteCode.Entity.News.Category", "Category")
                        .WithMany("NewsArticles")
                        .HasForeignKey("CategoryID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("LiteCode.Entity.News.NewsArticleText", b =>
                {
                    b.HasOne("LiteCode.Entity.News.NewsArticle", "NewsArticle")
                        .WithMany("NewsArticleTexts")
                        .HasForeignKey("ArticleID")
                        .OnDelete(DeleteBehavior.Cascade);
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
