using Microsoft.EntityFrameworkCore.Migrations;

namespace UturuAbiaNews.Data.Migrations
{
    public partial class NameRemoved : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Content",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Content",
                nullable: true);
        }
    }
}
