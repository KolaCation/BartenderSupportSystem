using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderSupportSystem.Server.Data.Migrations
{
    public partial class TestResultAndPickedAnswers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TestResultsSet",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomTestId = table.Column<int>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    PersonalMark = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestResultsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PickedAnswers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CustomTestResultId = table.Column<int>(nullable: false),
                    CustomAnswerId = table.Column<int>(nullable: false),
                    IsPicked = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PickedAnswers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PickedAnswers_TestResultsSet_CustomTestResultId",
                        column: x => x.CustomTestResultId,
                        principalTable: "TestResultsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PickedAnswers_CustomTestResultId",
                table: "PickedAnswers",
                column: "CustomTestResultId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PickedAnswers");

            migrationBuilder.DropTable(
                name: "TestResultsSet");
        }
    }
}
