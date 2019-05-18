using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HedgeLinks.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ArticleTopic",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArticleTopic", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ComercialTips",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    CreateDate = table.Column<string>(nullable: true),
                    EditDate = table.Column<string>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    Keyword = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComercialTips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ComercialTips_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ComercialTips_AspNetUsers_EditUserId",
                        column: x => x.EditUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ThirdSection",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FilePath = table.Column<string>(nullable: true),
                    Subtitle = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThirdSection", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Article",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ArticleTopicId = table.Column<int>(nullable: false),
                    AuthorName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    EditDate = table.Column<string>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    ExternalLink = table.Column<string>(nullable: true),
                    MenuPathId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    isShow = table.Column<bool>(nullable: false),
                    keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Article", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Article_AspNetUsers_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_ArticleTopic_ArticleTopicId",
                        column: x => x.ArticleTopicId,
                        principalTable: "ArticleTopic",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Article_AspNetUsers_EditUserId",
                        column: x => x.EditUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Article_MenuPath_MenuPathId",
                        column: x => x.MenuPathId,
                        principalTable: "MenuPath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Article_ApplicationUserId",
                table: "Article",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_ArticleTopicId",
                table: "Article",
                column: "ArticleTopicId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_EditUserId",
                table: "Article",
                column: "EditUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Article_MenuPathId",
                table: "Article",
                column: "MenuPathId");

            migrationBuilder.CreateIndex(
                name: "IX_ComercialTips_ApplicationUserId",
                table: "ComercialTips",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ComercialTips_EditUserId",
                table: "ComercialTips",
                column: "EditUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Article");

            migrationBuilder.DropTable(
                name: "ComercialTips");

            migrationBuilder.DropTable(
                name: "ThirdSection");

            migrationBuilder.DropTable(
                name: "ArticleTopic");
        }
    }
}
