using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHelperr.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class refactorResultToList : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Results_ResultId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ResultId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ResultValue",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Exercises");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Solutions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "ResultValues",
                table: "Results",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "Rank",
                table: "Level",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Results",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "ResultValues",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Rank",
                table: "Level");

            migrationBuilder.DropColumn(
                name: "Results",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ResultValue",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Level",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_ResultId",
                table: "Exercises",
                column: "ResultId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Results_ResultId",
                table: "Exercises",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "ResultId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
