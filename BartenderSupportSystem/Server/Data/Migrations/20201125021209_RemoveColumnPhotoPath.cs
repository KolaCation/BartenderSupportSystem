using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderSupportSystem.Server.Data.Migrations
{
    public partial class RemoveColumnPhotoPath : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "BartendersSet");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "BartendersSet",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
