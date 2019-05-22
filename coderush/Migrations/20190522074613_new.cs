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
                name: "TopImage",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreateDate = table.Column<string>(nullable: true),
                    CreatedUserId = table.Column<string>(nullable: true),
                    EditDate = table.Column<string>(nullable: true),
                    EditUserId = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    ImageSubtitle = table.Column<string>(nullable: true),
                    ImageTitle = table.Column<string>(nullable: true),
                    Keyword = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TopImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TopImage_AspNetUsers_CreatedUserId",
                        column: x => x.CreatedUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TopImage_AspNetUsers_EditUserId",
                        column: x => x.EditUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TopImage_CreatedUserId",
                table: "TopImage",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_TopImage_EditUserId",
                table: "TopImage",
                column: "EditUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TopImage");
        }
    }
}
