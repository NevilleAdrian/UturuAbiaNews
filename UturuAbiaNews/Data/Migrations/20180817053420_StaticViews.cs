using Microsoft.EntityFrameworkCore.Migrations;

namespace UturuAbiaNews.Data.Migrations
{
    public partial class StaticViews : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NoOfViews",
                table: "Content");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NoOfViews",
                table: "Content",
                nullable: false,
                defaultValue: 0);
        }
    }
}
