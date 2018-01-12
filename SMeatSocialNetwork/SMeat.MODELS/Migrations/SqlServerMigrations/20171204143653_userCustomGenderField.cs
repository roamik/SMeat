using Microsoft.EntityFrameworkCore.Migrations;

namespace SMeat.MODELS.Migrations.SqlServerMigrations
{
    public partial class userCustomGenderField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CustomGenderType",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CustomGenderType",
                table: "Users");
        }
    }
}
