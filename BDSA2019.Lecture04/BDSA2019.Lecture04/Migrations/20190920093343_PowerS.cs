using Microsoft.EntityFrameworkCore.Migrations;

namespace BDSA2019.Lecture04.Migrations
{
    public partial class PowerS : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperheroPowers_Power_PowerId",
                table: "SuperheroPowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Power",
                table: "Power");

            migrationBuilder.RenameTable(
                name: "Power",
                newName: "Powers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Powers",
                table: "Powers",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperheroPowers_Powers_PowerId",
                table: "SuperheroPowers",
                column: "PowerId",
                principalTable: "Powers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SuperheroPowers_Powers_PowerId",
                table: "SuperheroPowers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Powers",
                table: "Powers");

            migrationBuilder.RenameTable(
                name: "Powers",
                newName: "Power");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Power",
                table: "Power",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SuperheroPowers_Power_PowerId",
                table: "SuperheroPowers",
                column: "PowerId",
                principalTable: "Power",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
