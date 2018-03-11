using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    public partial class ReplyReply : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Replies_Replies_ReplyId",
                table: "Replies");

            migrationBuilder.DropTable(
                name: "BoardReply");

            migrationBuilder.DropIndex(
                name: "IX_Replies_ReplyId",
                table: "Replies");

            migrationBuilder.DropColumn(
                name: "ReplyId",
                table: "Replies");

            migrationBuilder.CreateTable(
                name: "ReplyReply",
                columns: table => new
                {
                    ReplyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReplyToId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyReply", x => new { x.ReplyId, x.ReplyToId });
                    table.ForeignKey(
                        name: "FK_ReplyReply_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ReplyReply_Replies_ReplyToId",
                        column: x => x.ReplyToId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyReply_ReplyToId",
                table: "ReplyReply",
                column: "ReplyToId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyReply");

            migrationBuilder.AddColumn<string>(
                name: "ReplyId",
                table: "Replies",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "BoardReply",
                columns: table => new
                {
                    BoardId = table.Column<string>(nullable: false),
                    ReplyId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BoardReply", x => new { x.BoardId, x.ReplyId });
                    table.ForeignKey(
                        name: "FK_BoardReply_Boards_BoardId",
                        column: x => x.BoardId,
                        principalTable: "Boards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BoardReply_Replies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "Replies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Replies_ReplyId",
                table: "Replies",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_BoardReply_ReplyId",
                table: "BoardReply",
                column: "ReplyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Replies_Replies_ReplyId",
                table: "Replies",
                column: "ReplyId",
                principalTable: "Replies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
