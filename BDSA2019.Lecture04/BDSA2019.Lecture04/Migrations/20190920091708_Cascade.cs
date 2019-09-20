using Microsoft.EntityFrameworkCore.Migrations;

namespace BDSA2019.Lecture04.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Superheroes_Cities_CityId",
                table: "Superheroes");

            migrationBuilder.AddForeignKey(
                name: "FK_Superheroes_Cities_CityId",
                table: "Superheroes",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Superheroes_Cities_CityId",
                table: "Superheroes");

            migrationBuilder.AddForeignKey(
                name: "FK_Superheroes_Cities_CityId",
                table: "Superheroes",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
