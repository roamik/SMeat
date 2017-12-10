using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SMeat.MODELS.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    About = table.Column<string>(nullable: true),
                    Birthdate = table.Column<DateTimeOffset>(nullable: true),
                    LastName = table.Column<string>(nullable: false),
                    LocationId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    PictureUrl = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    WorkplaceId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
