using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    public partial class LikesDislikes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Dislikes",
                table: "Boards");

            migrationBuilder.DropColumn(
                name: "Likes",
                table: "Boards");

            migrationBuilder.CreateTable(
                name: "BoardDislike",
                columns: table => new
                {
                    BoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    DislikeFromId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardDislike", x => new { x.BoardId, x.DislikeFromId });
                    table.ForeignKey(
                        name: "FK_BoardDislike_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardDislike_Users_DislikeFromId",
                        column: x => x.DislikeFromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BoardLike",
                columns: table => new
                {
                    BoardId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LikeFromId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardLike", x => new { x.BoardId, x.LikeFromId });
                    table.ForeignKey(
                        name: "FK_BoardLike_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardLike_Users_LikeFromId",
                        column: x => x.LikeFromId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BoardDislike_DislikeFromId",
                table: "BoardDislike",
                column: "DislikeFromId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardLike_LikeFromId",
                table: "BoardLike",
                column: "LikeFromId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BoardDislike");

            migrationBuilder.DropTable(
                name: "BoardLike");

            migrationBuilder.AddColumn<int>(
                name: "Dislikes",
                table: "Boards",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Likes",
                table: "Boards",
                nullable: false,
                defaultValue: 0);
        }
    }
}
