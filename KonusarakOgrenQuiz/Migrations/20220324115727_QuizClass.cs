using Microsoft.EntityFrameworkCore.Migrations;

namespace KonusarakOgrenQuiz.Migrations
{
    public partial class QuizClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_Wired_wiredId",
                table: "questions");

            migrationBuilder.DropIndex(
                name: "IX_questions_wiredId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "wiredId",
                table: "questions");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "wiredId",
                table: "questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_questions_wiredId",
                table: "questions",
                column: "wiredId");

            migrationBuilder.AddForeignKey(
                name: "FK_questions_Wired_wiredId",
                table: "questions",
                column: "wiredId",
                principalTable: "Wired",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
