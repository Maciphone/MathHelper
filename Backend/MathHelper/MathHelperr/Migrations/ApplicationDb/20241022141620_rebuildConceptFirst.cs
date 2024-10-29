using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MathHelperr.Migrations.ApplicationDb
{
    /// <inheritdoc />
    public partial class rebuildConceptFirst : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_Level_LevelId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Exercises_MathType_MathTypeId",
                table: "Exercises");

            migrationBuilder.DropForeignKey(
                name: "FK_Solutions_Results_ResultId",
                table: "Solutions");

            migrationBuilder.DropTable(
                name: "Level");

            migrationBuilder.DropTable(
                name: "MathType");

            migrationBuilder.DropIndex(
                name: "IX_Solutions_ResultId",
                table: "Solutions");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_LevelId",
                table: "Exercises");

            migrationBuilder.DropIndex(
                name: "IX_Exercises_MathTypeId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "ResultId",
                table: "Solutions");

            migrationBuilder.DropColumn(
                name: "LevelId",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MathTypeId",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ResultHash",
                table: "Results",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Level",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MathType",
                table: "Exercises",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ResultHash",
                table: "Results");

            migrationBuilder.DropColumn(
                name: "Level",
                table: "Exercises");

            migrationBuilder.DropColumn(
                name: "MathType",
                table: "Exercises");

            migrationBuilder.AddColumn<int>(
                name: "ResultId",
                table: "Solutions",
                type: "int",
                nullable: false,
                defaultValue: 0);

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
                    Rank = table.Column<int>(type: "int", nullable: false)
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
                name: "IX_Solutions_ResultId",
                table: "Solutions",
                column: "ResultId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Solutions_Results_ResultId",
                table: "Solutions",
                column: "ResultId",
                principalTable: "Results",
                principalColumn: "ResultId");
        }
    }
}
