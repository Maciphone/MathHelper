using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHelperr.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class AddExerciseTypeAndLevel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LevelId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MathTypeId",
                table: "Exercises",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Level",
                columns: table => new
                {
                    LevelId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Level", x => x.LevelId);
                });

            migrationBuilder.CreateTable(
                name: "MathType",
                columns: table => new
                {
                    MathTypeId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MathType", x => x.MathTypeId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_LevelId",
                table: "Exercises",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Exercises_MathTypeId",
                table: "Exercises",
                column: "MathTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_Level_LevelId",
                table: "Exercises",
                column: "LevelId",
                principalTable: "Level",
                principalColumn: "LevelId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Exercises_MathType_MathTypeId",
                table: "Exercises",
                column: "MathTypeId",
                principalTable: "MathType",
                principalColumn: "MathTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Level_LevelId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_MathType_MathTypeId",
                table: "Exercises");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "MathType");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_LevelId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_MathTypeId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MathTypeId",
                table: "Exercises");
        }
    }
}
