using Microsoft.EntityFrameworkCore.Migrations;

namespace FitFriends.Data.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagePath",
                table: "Categories");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagePath",
                table: "Categories",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
