using Microsoft.EntityFrameworkCore.Migrations;

namespace UturuAbiaNews.Data.Migrations
{
    public partial class AddedYoutubeUrl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VideoUrl",
                table: "Content",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VideoUrl",
                table: "Content");
        }
    }
}
