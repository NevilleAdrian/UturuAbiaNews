using Microsoft.EntityFrameworkCore.Migrations;

namespace UturuAbiaNews.Data.Migrations
{
    public partial class AddedCommentName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Comment",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Comment",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Comment");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Comment");
        }
    }
}
