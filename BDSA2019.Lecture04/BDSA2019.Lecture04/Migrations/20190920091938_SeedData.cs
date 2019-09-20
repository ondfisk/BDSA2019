using Microsoft.EntityFrameworkCore.Migrations;

namespace BDSA2019.Lecture04.Migrations
{
    public partial class SeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Cities",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Metropolis" },
                    { 2, "Gotham City" },
                    { 3, "Atlantis" },
                    { 4, "Themyscira" },
                    { 5, "New York City" },
                    { 6, "Central City" }
                });

            migrationBuilder.InsertData(
                table: "Superheroes",
                columns: new[] { "Id", "AlterEgo", "CityId", "FirstAppearance", "Gender", "Name", "Occupation" },
                values: new object[,]
                {
                    { 1, "Aquaman", 1, 1941, 1, "Arthur Curry", "King of Atlantis" },
                    { 2, "Superman", 1, 1938, 1, "Clark Kent", "Reporter" },
                    { 3, "Batman", 2, 1939, 1, "Bruce Wayne", "CEO of Wayne Enterprises" },
                    { 7, "Catwoman", 2, 1940, 0, "Selina Kyle", "Thief" },
                    { 8, "Batwoman", 2, 1956, 0, "Kate Kane", "Thief" },
                    { 4, "Wonder Woman", 4, 1941, 0, "Diana", "Amazon Princess" },
                    { 5, "Green Lantern", 5, 1940, 1, "Hal Jordan", "Test pilot" },
                    { 9, "Supergirl", 5, 1959, 0, "Kara Zor-El", "Actress" },
                    { 6, "The Flash", 6, 1940, 1, "Barry Allen", "Forensic scientist" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Superheroes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Cities",
                keyColumn: "Id",
                keyValue: 6);
        }
    }
}
