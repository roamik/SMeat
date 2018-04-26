using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    public partial class MadeTime_for_boards : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardDislike_Boards_BoardId",
                table: "BoardDislike");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardLike_Boards_BoardId",
                table: "BoardLike");

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "MadeTime",
                table: "Boards",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddForeignKey(
                name: "FK_BoardDislike_Boards_BoardId",
                table: "BoardDislike",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardLike_Boards_BoardId",
                table: "BoardLike",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoardDislike_Boards_BoardId",
                table: "BoardDislike");

            migrationBuilder.DropForeignKey(
                name: "FK_BoardLike_Boards_BoardId",
                table: "BoardLike");

            migrationBuilder.DropColumn(
                name: "MadeTime",
                table: "Boards");

            migrationBuilder.AddForeignKey(
                name: "FK_BoardDislike_Boards_BoardId",
                table: "BoardDislike",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BoardLike_Boards_BoardId",
                table: "BoardLike",
                column: "BoardId",
                principalTable: "Boards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
