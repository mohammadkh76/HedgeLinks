using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HedgeLinks.Migrations
{
    public partial class @new : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreateDate",
                table: "ArticleTopic",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "ArticleTopic",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditDate",
                table: "ArticleTopic",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditUserId",
                table: "ArticleTopic",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTopic_CreatedUserId",
                table: "ArticleTopic",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_ArticleTopic_EditUserId",
                table: "ArticleTopic",
                column: "EditUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTopic_AspNetUsers_CreatedUserId",
                table: "ArticleTopic",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ArticleTopic_AspNetUsers_EditUserId",
                table: "ArticleTopic",
                column: "EditUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTopic_AspNetUsers_CreatedUserId",
                table: "ArticleTopic");

            migrationBuilder.DropForeignKey(
                name: "FK_ArticleTopic_AspNetUsers_EditUserId",
                table: "ArticleTopic");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTopic_CreatedUserId",
                table: "ArticleTopic");

            migrationBuilder.DropIndex(
                name: "IX_ArticleTopic_EditUserId",
                table: "ArticleTopic");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "ArticleTopic");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "ArticleTopic");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "ArticleTopic");

            migrationBuilder.DropColumn(
                name: "EditUserId",
                table: "ArticleTopic");
        }
    }
}
