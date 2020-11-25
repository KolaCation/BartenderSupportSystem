using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderSupportSystem.Server.Data.Migrations
{
    public partial class AddColumnAuthorUsername : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AuthorUsername",
                table: "TestsSet",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AuthorUsername",
                table: "TestsSet");
        }
    }
}
