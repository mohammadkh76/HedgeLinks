using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class second : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MenuPath",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Description = table.Column<string>(nullable: true),
                    FilePath = table.Column<string>(nullable: true),
                    PageName = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuPath", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menubar",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuPathId = table.Column<int>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menubar", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menubar_MenuPath_MenuPathId",
                        column: x => x.MenuPathId,
                        principalTable: "MenuPath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Submenu",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    MenuPathId = table.Column<int>(nullable: true),
                    MenubarId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Path = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Submenu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Submenu_MenuPath_MenuPathId",
                        column: x => x.MenuPathId,
                        principalTable: "MenuPath",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Submenu_Menubar_MenubarId",
                        column: x => x.MenubarId,
                        principalTable: "Menubar",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menubar_MenuPathId",
                table: "Menubar",
                column: "MenuPathId");

            migrationBuilder.CreateIndex(
                name: "IX_Submenu_MenuPathId",
                table: "Submenu",
                column: "MenuPathId");

            migrationBuilder.CreateIndex(
                name: "IX_Submenu_MenubarId",
                table: "Submenu",
                column: "MenubarId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Submenu");

            migrationBuilder.DropTable(
                name: "Menubar");

            migrationBuilder.DropTable(
                name: "MenuPath");
        }
    }
}
