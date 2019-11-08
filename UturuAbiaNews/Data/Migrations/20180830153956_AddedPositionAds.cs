using Microsoft.EntityFrameworkCore.Migrations;

namespace UturuAbiaNews.Data.Migrations
{
    public partial class AddedPositionAds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Content");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Advertisement",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Position",
                table: "Advertisement");

            migrationBuilder.AddColumn<int>(
                name: "Position",
                table: "Content",
                nullable: false,
                defaultValue: 0);
        }
    }
}
