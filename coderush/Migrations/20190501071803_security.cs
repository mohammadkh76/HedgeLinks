using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class security : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FilePath",
                table: "MenuPath",
                newName: "LastEditDate");

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "MenuPath",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedDate",
                table: "MenuPath",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditBy",
                table: "MenuPath",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "MenuPath");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "MenuPath");

            migrationBuilder.DropColumn(
                name: "EditBy",
                table: "MenuPath");

            migrationBuilder.RenameColumn(
                name: "LastEditDate",
                table: "MenuPath",
                newName: "FilePath");
        }
    }
}
