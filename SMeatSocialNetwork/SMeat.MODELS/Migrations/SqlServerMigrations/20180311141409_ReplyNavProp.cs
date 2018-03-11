using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    public partial class ReplyNavProp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReplyId",
                table: "Replies",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ReplyId",
                table: "Replies",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ReplyId",
                table: "Replies",
                column: "ReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ReplyId",
                table: "Replies");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ReplyId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Replies");
        }
    }
}
