using Microsoft.EntityFrameworkCore.Migrations;

namespace KonusarakOgrenQuiz.Migrations
{
    public partial class dangareous : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionList_QuestionListid",
                table: "questions");

            migrationBuilder.DropForeignKey(
                name: "FK_questions_Quiz_quizId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_quizId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "quizId",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "QuestionListid",
                table: "questions",
                newName: "questionListId");

            migrationBuilder.RenameIndex(
                name: "IX_questions_QuestionListid",
                table: "questions",
                newName: "IX_questions_questionListId");

            migrationBuilder.AlterColumn<int>(
                name: "questionListId",
                table: "questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionList_questionListId",
                table: "questions",
                column: "questionListId",
                principalTable: "questionList",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_questionList_questionListId",
                table: "questions");

            migrationBuilder.RenameColumn(
                name: "questionListId",
                table: "questions",
                newName: "QuestionListid");

            migrationBuilder.RenameIndex(
                name: "IX_questions_questionListId",
                table: "questions",
                newName: "IX_questions_QuestionListid");

            migrationBuilder.AlterColumn<int>(
                name: "QuestionListid",
                table: "questions",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddColumn<int>(
                name: "quizId",
                table: "questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questions_quizId",
                table: "questions",
                column: "quizId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_questionList_QuestionListid",
                table: "questions",
                column: "QuestionListid",
                principalTable: "questionList",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_questions_Quiz_quizId",
                table: "questions",
                column: "quizId",
                principalTable: "Quiz",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
