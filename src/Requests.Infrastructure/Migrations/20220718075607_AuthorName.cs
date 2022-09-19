using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Requests.Infrastructure.Migrations
{
    public partial class AuthorName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorName",
                table: "Requests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: string.Empty);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorName",
                table: "Requests");
        }
    }
}
