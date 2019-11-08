using Microsoft.EntityFrameworkCore.Migrations;

namespace UturuAbiaNews.Data.Migrations
{
    public partial class ContentDbChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentedMessage",
                table: "Content",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Content",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "Content",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedMessage",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "Email",
                table: "Content");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "Content");
        }
    }
}
