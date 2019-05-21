using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace HedgeLinks.Migrations
{
    public partial class detail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_ApplicationUserId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_ComercialTips_AspNetUsers_ApplicationUserId",
                table: "ComercialTips");

            migrationBuilder.DropForeignKey(
                name: "FK_Menubar_AspNetUsers_ApplicationUserId",
                table: "Menubar");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPath_AspNetUsers_ApplicationUserId",
                table: "MenuPath");

            migrationBuilder.DropForeignKey(
                name: "FK_Submenu_AspNetUsers_ApplicationUserId",
                table: "Submenu");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Submenu",
                newName: "CreatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submenu_ApplicationUserId",
                table: "Submenu",
                newName: "IX_Submenu_CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "MenuPath",
                newName: "CreatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuPath_ApplicationUserId",
                table: "MenuPath",
                newName: "IX_MenuPath_CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Menubar",
                newName: "CreatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Menubar_ApplicationUserId",
                table: "Menubar",
                newName: "IX_Menubar_CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "ComercialTips",
                newName: "CreatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ComercialTips_ApplicationUserId",
                table: "ComercialTips",
                newName: "IX_ComercialTips_CreatedUserId");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Article",
                newName: "CreatedUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_ApplicationUserId",
                table: "Article",
                newName: "IX_Article_CreatedUserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "UserProfile",
                nullable: false,
                oldClrType: typeof(int))
                .OldAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserProfile",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "CreateDate",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedUserId",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditDate",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "EditUserId",
                table: "UserProfile",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_CreatedUserId",
                table: "UserProfile",
                column: "CreatedUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserProfile_EditUserId",
                table: "UserProfile",
                column: "EditUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_CreatedUserId",
                table: "Article",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComercialTips_AspNetUsers_CreatedUserId",
                table: "ComercialTips",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menubar_AspNetUsers_CreatedUserId",
                table: "Menubar",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPath_AspNetUsers_CreatedUserId",
                table: "MenuPath",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submenu_AspNetUsers_CreatedUserId",
                table: "Submenu",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_AspNetUsers_CreatedUserId",
                table: "UserProfile",
                column: "CreatedUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfile_AspNetUsers_EditUserId",
                table: "UserProfile",
                column: "EditUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Article_AspNetUsers_CreatedUserId",
                table: "Article");

            migrationBuilder.DropForeignKey(
                name: "FK_ComercialTips_AspNetUsers_CreatedUserId",
                table: "ComercialTips");

            migrationBuilder.DropForeignKey(
                name: "FK_Menubar_AspNetUsers_CreatedUserId",
                table: "Menubar");

            migrationBuilder.DropForeignKey(
                name: "FK_MenuPath_AspNetUsers_CreatedUserId",
                table: "MenuPath");

            migrationBuilder.DropForeignKey(
                name: "FK_Submenu_AspNetUsers_CreatedUserId",
                table: "Submenu");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_AspNetUsers_CreatedUserId",
                table: "UserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_UserProfile_AspNetUsers_EditUserId",
                table: "UserProfile");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_CreatedUserId",
                table: "UserProfile");

            migrationBuilder.DropIndex(
                name: "IX_UserProfile_EditUserId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "CreatedUserId",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "EditDate",
                table: "UserProfile");

            migrationBuilder.DropColumn(
                name: "EditUserId",
                table: "UserProfile");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Submenu",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Submenu_CreatedUserId",
                table: "Submenu",
                newName: "IX_Submenu_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "MenuPath",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_MenuPath_CreatedUserId",
                table: "MenuPath",
                newName: "IX_MenuPath_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Menubar",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Menubar_CreatedUserId",
                table: "Menubar",
                newName: "IX_Menubar_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "ComercialTips",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_ComercialTips_CreatedUserId",
                table: "ComercialTips",
                newName: "IX_ComercialTips_ApplicationUserId");

            migrationBuilder.RenameColumn(
                name: "CreatedUserId",
                table: "Article",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Article_CreatedUserId",
                table: "Article",
                newName: "IX_Article_ApplicationUserId");

            migrationBuilder.AlterColumn<int>(
                name: "UserProfileId",
                table: "UserProfile",
                nullable: false,
                oldClrType: typeof(int))
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserProfile",
                table: "UserProfile",
                column: "UserProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_Article_AspNetUsers_ApplicationUserId",
                table: "Article",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ComercialTips_AspNetUsers_ApplicationUserId",
                table: "ComercialTips",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Menubar_AspNetUsers_ApplicationUserId",
                table: "Menubar",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MenuPath_AspNetUsers_ApplicationUserId",
                table: "MenuPath",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Submenu_AspNetUsers_ApplicationUserId",
                table: "Submenu",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
