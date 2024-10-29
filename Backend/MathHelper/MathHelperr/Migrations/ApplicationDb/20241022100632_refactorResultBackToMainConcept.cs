using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHelperr.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class refactorResultBackToMainConcept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Results",
                table: "Exercises");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Results_ResultId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_ResultId",
                table: "Exercises");

            migrationBuilder.AddColumn<string>(
                name: "Results",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
