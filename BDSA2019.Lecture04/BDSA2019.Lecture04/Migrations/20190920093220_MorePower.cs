using Microsoft.EntityFrameworkCore.Migrations;

namespace BDSA2019.Lecture04.Migrations
{
    public partial class MorePower : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Power",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "ability to breathe underwater" },
                    { 26, "superhuman agility" },
                    { 25, "super strength" },
                    { 24, "super speed" },
                    { 23, "magic weaponry" },
                    { 22, "invulnerability" },
                    { 21, "intelligence" },
                    { 20, "intangibility" },
                    { 19, "instant weaponry" },
                    { 18, "inexhaustible wealth" },
                    { 17, "heat vision" },
                    { 16, "healing factor" },
                    { 15, "hard light constructs" },
                    { 14, "gymnastic ability" },
                    { 13, "freeze breath" },
                    { 12, "force fields" },
                    { 11, "flight" },
                    { 10, "exceptional swimming ability" },
                    { 9, "exceptional martial artist" },
                    { 8, "durability" },
                    { 7, "control over sea life" },
                    { 6, "combat strategy" },
                    { 5, "combat skill" },
                    { 4, "brilliant deductive skills" },
                    { 3, "alien technology" },
                    { 2, "advanced technology" },
                    { 27, "superhuman hearing" },
                    { 28, "x-ray vision" }
                });

            migrationBuilder.InsertData(
                table: "SuperheroPowers",
                columns: new[] { "SuperheroId", "PowerId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 4, 16 },
                    { 9, 16 },
                    { 2, 17 },
                    { 9, 17 },
                    { 3, 18 },
                    { 5, 19 },
                    { 6, 20 },
                    { 8, 21 },
                    { 2, 22 },
                    { 4, 22 },
                    { 9, 22 },
                    { 4, 23 },
                    { 2, 24 },
                    { 6, 24 },
                    { 9, 24 },
                    { 1, 25 },
                    { 2, 25 },
                    { 4, 25 },
                    { 9, 25 },
                    { 4, 26 },
                    { 6, 26 },
                    { 2, 27 },
                    { 9, 27 },
                    { 2, 16 },
                    { 5, 15 },
                    { 7, 14 },
                    { 9, 13 },
                    { 3, 2 },
                    { 8, 2 },
                    { 5, 3 },
                    { 3, 4 },
                    { 8, 4 },
                    { 4, 5 },
                    { 7, 5 },
                    { 8, 5 },
                    { 3, 6 },
                    { 4, 6 },
                    { 8, 6 },
                    { 2, 28 },
                    { 1, 7 },
                    { 5, 8 },
                    { 3, 9 },
                    { 7, 9 },
                    { 8, 9 },
                    { 1, 10 },
                    { 2, 11 },
                    { 4, 11 },
                    { 5, 11 },
                    { 9, 11 },
                    { 5, 12 },
                    { 2, 13 },
                    { 1, 8 },
                    { 9, 28 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 1, 7 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 1, 8 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 1, 10 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 1, 25 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 11 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 13 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 16 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 17 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 22 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 24 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 25 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 27 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 2, 28 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 3, 2 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 3, 4 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 3, 6 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 3, 9 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 3, 18 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 5 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 6 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 11 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 16 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 22 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 23 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 25 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 4, 26 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 5, 3 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 5, 8 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 5, 11 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 5, 12 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 5, 15 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 5, 19 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 6, 20 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 6, 24 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 6, 26 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 7, 5 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 7, 9 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 7, 14 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 8, 4 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 8, 5 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 8, 6 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 8, 9 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 8, 21 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 11 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 13 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 16 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 17 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 22 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 24 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 25 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 27 });

            migrationBuilder.DeleteData(
                table: "SuperheroPowers",
                keyColumns: new[] { "SuperheroId", "PowerId" },
                keyValues: new object[] { 9, 28 });

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 20);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 21);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 22);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 23);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 24);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 25);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 26);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 27);

            migrationBuilder.DeleteData(
                table: "Power",
                keyColumn: "Id",
                keyValue: 28);
        }
    }
}
