using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderSupportSystem.Server.Data.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "BartenderId",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "RegistrationDate",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.CreateTable(
                name: "BartendersSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Experience = table.Column<double>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BartendersSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BrandsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    CountryOfOrigin = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BrandsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CocktailsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CocktailsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DrinksSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    AlcoholPercentage = table.Column<double>(nullable: false),
                    Flavor = table.Column<string>(nullable: true),
                    BrandId = table.Column<Guid>(nullable: false),
                    PricePerMl = table.Column<double>(nullable: false),
                    PhotoPath = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DrinksSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "IngredientsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ComponentId = table.Column<Guid>(nullable: false),
                    CocktailId = table.Column<Guid>(nullable: false),
                    Weight = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IngredientsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MenusSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    DrinkId = table.Column<Guid>(nullable: false),
                    SnackId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenusSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PricePerGr = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    TestId = table.Column<Guid>(nullable: false),
                    Mark = table.Column<double>(nullable: false),
                    QuantityOfRaters = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnacksSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PricePerGr = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnacksSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TestsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Topic = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TestsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QuestionsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Statement = table.Column<string>(nullable: true),
                    TestId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsSet_TestsSet_TestId",
                        column: x => x.TestId,
                        principalTable: "TestsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnswersSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Statement = table.Column<string>(nullable: true),
                    IsCorrect = table.Column<bool>(nullable: false),
                    QuestionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnswersSet", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnswersSet_QuestionsSet_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "QuestionsSet",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnswersSet_QuestionId",
                table: "AnswersSet",
                column: "QuestionId");

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsSet_TestId",
                table: "QuestionsSet",
                column: "TestId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnswersSet");

            migrationBuilder.DropTable(
                name: "BartendersSet");

            migrationBuilder.DropTable(
                name: "BrandsSet");

            migrationBuilder.DropTable(
                name: "CocktailsSet");

            migrationBuilder.DropTable(
                name: "DrinksSet");

            migrationBuilder.DropTable(
                name: "IngredientsSet");

            migrationBuilder.DropTable(
                name: "MenusSet");

            migrationBuilder.DropTable(
                name: "ProductsSet");

            migrationBuilder.DropTable(
                name: "RatingsSet");

            migrationBuilder.DropTable(
                name: "SnacksSet");

            migrationBuilder.DropTable(
                name: "QuestionsSet");

            migrationBuilder.DropTable(
                name: "TestsSet");

            migrationBuilder.DropColumn(
                name: "BartenderId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RegistrationDate",
                table: "AspNetUsers");
        }
    }
}
