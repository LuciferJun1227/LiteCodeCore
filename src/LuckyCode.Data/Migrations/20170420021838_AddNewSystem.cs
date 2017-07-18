using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace LuckyCode.Data.Migrations
{
    public partial class AddNewSystem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Category",
                columns: table => new
                {
                    CategoryID = table.Column<string>(maxLength: 30, nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CategoryType = table.Column<string>(maxLength: 50, nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    DisplayOrder = table.Column<int>(nullable: false),
                    HyperLink = table.Column<string>(maxLength: 250, nullable: true),
                    ParentID = table.Column<string>(maxLength: 30, nullable: false),
                    SortCode = table.Column<string>(maxLength: 10, nullable: true),
                    Title = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Category", x => x.CategoryID);
                });

            migrationBuilder.CreateTable(
                name: "Links",
                columns: table => new
                {
                    LinkID = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    DisplayOrder = table.Column<int>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 150, nullable: true),
                    IsImage = table.Column<bool>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(maxLength: 150, nullable: true),
                    UserEmail = table.Column<string>(maxLength: 150, nullable: true),
                    UserName = table.Column<string>(maxLength: 50, nullable: true),
                    UserTel = table.Column<string>(maxLength: 50, nullable: true),
                    WebUrl = table.Column<string>(maxLength: 150, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Links", x => x.LinkID);
                });

            migrationBuilder.CreateTable(
                name: "NewsBanner",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    CreateTime = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(maxLength: 256, nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Sort = table.Column<int>(nullable: false),
                    Title = table.Column<string>(maxLength: 128, nullable: false),
                    Url = table.Column<string>(maxLength: 256, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsBanner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticles",
                columns: table => new
                {
                    ArticleID = table.Column<Guid>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    Author = table.Column<string>(maxLength: 50, nullable: true),
                    CategoryID = table.Column<string>(maxLength: 30, nullable: false),
                    ClickNum = table.Column<int>(nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    Editor = table.Column<string>(maxLength: 50, nullable: true),
                    ImgUrl = table.Column<string>(maxLength: 250, nullable: true),
                    IsCommend = table.Column<bool>(nullable: false),
                    IsComment = table.Column<bool>(nullable: false),
                    IsHot = table.Column<bool>(nullable: false),
                    IsImage = table.Column<bool>(nullable: false),
                    IsLock = table.Column<bool>(nullable: false),
                    IsSlide = table.Column<bool>(nullable: false),
                    IsTop = table.Column<bool>(nullable: false),
                    KeyWord = table.Column<string>(maxLength: 150, nullable: true),
                    OrgID = table.Column<int>(nullable: true),
                    Source = table.Column<string>(maxLength: 150, nullable: true),
                    Summarize = table.Column<string>(maxLength: 500, nullable: true),
                    Title = table.Column<string>(maxLength: 250, nullable: false),
                    UpdateDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    UserID = table.Column<string>(maxLength: 128, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticles", x => x.ArticleID);
                    table.ForeignKey(
                        name: "FK_NewsArticles_Category_CategoryID",
                        column: x => x.CategoryID,
                        principalTable: "Category",
                        principalColumn: "CategoryID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NewsArticleText",
                columns: table => new
                {
                    ArticleTextID = table.Column<long>(nullable: false)
                        .Annotation("MySql:ValueGeneratedOnAdd", true),
                    ArticleID = table.Column<Guid>(nullable: false),
                    ArticleText = table.Column<string>(type: "text", nullable: true),
                    NoHtml = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NewsArticleText", x => x.ArticleTextID);
                    table.ForeignKey(
                        name: "FK_NewsArticleText_NewsArticles_ArticleID",
                        column: x => x.ArticleID,
                        principalTable: "NewsArticles",
                        principalColumn: "ArticleID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticles_CategoryID",
                table: "NewsArticles",
                column: "CategoryID");

            migrationBuilder.CreateIndex(
                name: "IX_NewsArticleText_ArticleID",
                table: "NewsArticleText",
                column: "ArticleID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Links");

            migrationBuilder.DropTable(
                name: "NewsArticleText");

            migrationBuilder.DropTable(
                name: "NewsBanner");

            migrationBuilder.DropTable(
                name: "NewsArticles");

            migrationBuilder.DropTable(
                name: "Category");
        }
    }
}
