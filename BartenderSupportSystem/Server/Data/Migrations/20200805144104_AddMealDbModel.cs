using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BartenderSupportSystem.Server.Data.Migrations
{
    public partial class AddMealDbModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductsSet");

            migrationBuilder.DropTable(
                name: "SnacksSet");

            migrationBuilder.DropColumn(
                name: "SnackId",
                table: "MenusSet");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "IngredientsSet");

            migrationBuilder.AddColumn<Guid>(
                name: "MealId",
                table: "MenusSet",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "ProportionType",
                table: "IngredientsSet",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "ProportionValue",
                table: "IngredientsSet",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "CreationDates",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ItemId = table.Column<Guid>(nullable: false),
                    CreationTime = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CreationDates", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MealsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PricePerGr = table.Column<double>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MealsSet", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CreationDates");

            migrationBuilder.DropTable(
                name: "MealsSet");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "MenusSet");

            migrationBuilder.DropColumn(
                name: "ProportionType",
                table: "IngredientsSet");

            migrationBuilder.DropColumn(
                name: "ProportionValue",
                table: "IngredientsSet");

            migrationBuilder.AddColumn<Guid>(
                name: "SnackId",
                table: "MenusSet",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "IngredientsSet",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateTable(
                name: "Cocktail",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoPath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cocktail", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductsSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerGr = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductsSet", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SnacksSet",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PricePerGr = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SnacksSet", x => x.Id);
                });
        }
    }
}
