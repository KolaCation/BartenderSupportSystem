using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderSupportSystem.Server.Data.Migrations
{
    public partial class AddUserRatingSet : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Mark",
                table: "RatingsSet");

            migrationBuilder.DropColumn(
                name: "QuantityOfRaters",
                table: "RatingsSet");

            migrationBuilder.CreateTable(
                name: "UserRatingsSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RatingId = table.Column<int>(nullable: false),
                    TestId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    Mark = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRatingsSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRatingsSet_RatingsSet_RatingId",
                        column: x => x.RatingId,
                        principalTable: "RatingsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserRatingsSet_RatingId",
                table: "UserRatingsSet",
                column: "RatingId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRatingsSet");

            migrationBuilder.AddColumn<double>(
                name: "Mark",
                table: "RatingsSet",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "QuantityOfRaters",
                table: "RatingsSet",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
