using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HedgeLinks.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_MenuPath_MenuPathId",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "MenuPathId",
                table: "Job",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Job_MenuPath_MenuPathId",
                table: "Job",
                column: "MenuPathId",
                principalTable: "MenuPath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Job_MenuPath_MenuPathId",
                table: "Job");

            migrationBuilder.AlterColumn<int>(
                name: "MenuPathId",
                table: "Job",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Job_MenuPath_MenuPathId",
                table: "Job",
                column: "MenuPathId",
                principalTable: "MenuPath",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
