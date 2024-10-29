using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHelperr.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class refactorResultToListWithResultId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Exercises");
        }
    }
}
