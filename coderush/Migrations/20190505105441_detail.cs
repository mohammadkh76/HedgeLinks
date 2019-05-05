using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace coderush.Migrations
{
    public partial class detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastEditDate",
                table: "MenuPath",
                newName: "EditedBy");

            migrationBuilder.RenameColumn(
                name: "EditBy",
                table: "MenuPath",
                newName: "EditDate");

            migrationBuilder.RenameColumn(
                name: "CreatedDate",
                table: "MenuPath",
                newName: "CreateDate");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "EditedBy",
                table: "MenuPath",
                newName: "LastEditDate");

            migrationBuilder.RenameColumn(
                name: "EditDate",
                table: "MenuPath",
                newName: "EditBy");

            migrationBuilder.RenameColumn(
                name: "CreateDate",
                table: "MenuPath",
                newName: "CreatedDate");
        }
    }
}
