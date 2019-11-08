using Microsoft.EntityFrameworkCore.Migrations;

namespace BDSA2019.Lecture09.Entities.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Powers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Powers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Superheroes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    AlterEgo = table.Column<string>(maxLength: 50, nullable: false),
                    Occupation = table.Column<string>(maxLength: 50, nullable: true),
                    CityId = table.Column<int>(nullable: true),
                    Gender = table.Column<string>(maxLength: 50, nullable: false),
                    FirstAppearance = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Superheroes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Superheroes_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "SuperheroPowers",
                columns: table => new
                {
                    SuperheroId = table.Column<int>(nullable: false),
                    PowerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SuperheroPowers", x => new { x.SuperheroId, x.PowerId });
                    table.ForeignKey(
                        name: "FK_SuperheroPowers_Powers_PowerId",
                        column: x => x.PowerId,
                        principalTable: "Powers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SuperheroPowers_Superheroes_SuperheroId",
                        column: x => x.SuperheroId,
                        principalTable: "Superheroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

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
                table: "Powers",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 16, "healing factor" },
                    { 17, "heat vision" },
                    { 18, "inexhaustible wealth" },
                    { 19, "instant weaponry" },
                    { 20, "intangibility" },
                    { 23, "magic weaponry" },
                    { 22, "invulnerability" },
                    { 15, "hard light constructs" },
                    { 24, "super speed" },
                    { 25, "super strength" },
                    { 26, "superhuman agility" },
                    { 21, "intelligence" },
                    { 14, "gymnastic ability" },
                    { 11, "flight" },
                    { 12, "force fields" },
                    { 27, "superhuman hearing" },
                    { 10, "exceptional swimming ability" },
                    { 9, "exceptional martial artist" },
                    { 8, "durability" },
                    { 7, "control over sea life" },
                    { 6, "combat strategy" },
                    { 5, "combat skill" },
                    { 4, "brilliant deductive skills" },
                    { 3, "alien technology" },
                    { 2, "advanced technology" },
                    { 1, "ability to breathe underwater" },
                    { 13, "freeze breath" },
                    { 28, "x-ray vision" }
                });

            migrationBuilder.InsertData(
                table: "Superheroes",
                columns: new[] { "Id", "AlterEgo", "CityId", "FirstAppearance", "Gender", "Name", "Occupation" },
                values: new object[,]
                {
                    { 1, "Aquaman", 1, 1941, "Male", "Arthur Curry", "King of Atlantis" },
                    { 2, "Superman", 1, 1938, "Male", "Clark Kent", "Reporter" },
                    { 3, "Batman", 2, 1939, "Male", "Bruce Wayne", "CEO of Wayne Enterprises" },
                    { 7, "Catwoman", 2, 1940, "Female", "Selina Kyle", "Thief" },
                    { 8, "Batwoman", 2, 1956, "Female", "Kate Kane", "Thief" },
                    { 4, "Wonder Woman", 4, 1941, "Female", "Diana", "Amazon Princess" },
                    { 5, "Green Lantern", 5, 1940, "Male", "Hal Jordan", "Test pilot" },
                    { 9, "Supergirl", 5, 1959, "Female", "Kara Zor-El", "Actress" },
                    { 6, "The Flash", 6, 1940, "Male", "Barry Allen", "Forensic scientist" }
                });

            migrationBuilder.InsertData(
                table: "SuperheroPowers",
                columns: new[] { "SuperheroId", "PowerId" },
                values: new object[,]
                {
                    { 1, 25 },
                    { 4, 22 },
                    { 4, 11 },
                    { 4, 5 },
                    { 4, 6 },
                    { 4, 26 },
                    { 4, 16 },
                    { 4, 23 },
                    { 5, 15 },
                    { 5, 19 },
                    { 5, 12 },
                    { 5, 11 },
                    { 5, 8 },
                    { 5, 3 },
                    { 9, 25 },
                    { 9, 11 },
                    { 9, 22 },
                    { 9, 24 },
                    { 9, 17 },
                    { 9, 13 },
                    { 9, 28 },
                    { 9, 27 },
                    { 9, 16 },
                    { 6, 24 },
                    { 4, 25 },
                    { 8, 2 },
                    { 8, 21 },
                    { 8, 4 },
                    { 1, 8 },
                    { 1, 7 },
                    { 1, 10 },
                    { 1, 1 },
                    { 2, 25 },
                    { 2, 11 },
                    { 2, 22 },
                    { 2, 24 },
                    { 2, 17 },
                    { 2, 13 },
                    { 2, 28 },
                    { 6, 20 },
                    { 2, 27 },
                    { 3, 9 },
                    { 3, 6 },
                    { 3, 18 },
                    { 3, 4 },
                    { 3, 2 },
                    { 7, 9 },
                    { 7, 14 },
                    { 7, 5 },
                    { 8, 9 },
                    { 8, 6 },
                    { 8, 5 },
                    { 2, 16 },
                    { 6, 26 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cities_Name",
                table: "Cities",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Powers_Name",
                table: "Powers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Superheroes_CityId",
                table: "Superheroes",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_SuperheroPowers_PowerId",
                table: "SuperheroPowers",
                column: "PowerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SuperheroPowers");

            migrationBuilder.DropTable(
                name: "Powers");

            migrationBuilder.DropTable(
                name: "Superheroes");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
