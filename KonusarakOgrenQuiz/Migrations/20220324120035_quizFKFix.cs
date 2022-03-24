using Microsoft.EntityFrameworkCore.Migrations;

namespace KonusarakOgrenQuiz.Migrations
{
    public partial class quizFKFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "quizId",
                table: "questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Quiz",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    wiredId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Quiz", x => x.id);
                    table.ForeignKey(
                        name: "FK_Quiz_Wired_wiredId",
                        column: x => x.wiredId,
                        principalTable: "Wired",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_quizId",
                table: "questions",
                column: "quizId");

            migrationBuilder.CreateIndex(
                name: "IX_Quiz_wiredId",
                table: "Quiz",
                column: "wiredId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_Quiz_quizId",
                table: "questions",
                column: "quizId",
                principalTable: "Quiz",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_Quiz_quizId",
                table: "questions");

            migrationBuilder.DropTable(
                name: "Quiz");

            migrationBuilder.DropIndex(
                name: "IX_questions_quizId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "quizId",
                table: "questions");
        }
    }
}
