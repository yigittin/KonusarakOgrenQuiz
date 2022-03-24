using Microsoft.EntityFrameworkCore.Migrations;

namespace KonusarakOgrenQuiz.Migrations
{
    public partial class tryingSomething : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "QuestionListid",
                table: "questions",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "questionList",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    quizId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_questionList", x => x.id);
                    table.ForeignKey(
                        name: "FK_questionList_Quiz_quizId",
                        column: x => x.quizId,
                        principalTable: "Quiz",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_questions_QuestionListid",
                table: "questions",
                column: "QuestionListid");

            migrationBuilder.CreateIndex(
                name: "IX_questionList_quizId",
                table: "questionList",
                column: "quizId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionList_QuestionListid",
                table: "questions",
                column: "QuestionListid",
                principalTable: "questionList",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionList_QuestionListid",
                table: "questions");

            migrationBuilder.DropTable(
                name: "questionList");

            migrationBuilder.DropIndex(
                name: "IX_questions_QuestionListid",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "QuestionListid",
                table: "questions");
        }
    }
}
