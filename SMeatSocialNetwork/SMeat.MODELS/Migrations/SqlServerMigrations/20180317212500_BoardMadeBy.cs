using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    public partial class BoardMadeBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MadeById",
                table: "Boards",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Boards_MadeById",
                table: "Boards",
                column: "MadeById");

            migrationBuilder.AddForeignKey(
                name: "FK_Boards_Users_MadeById",
                table: "Boards",
                column: "MadeById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Boards_Users_MadeById",
                table: "Boards");

            migrationBuilder.DropIndex(
                name: "IX_Boards_MadeById",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "MadeById",
                table: "Boards");
        }
    }
}
