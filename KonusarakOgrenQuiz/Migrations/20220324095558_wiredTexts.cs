using Microsoft.EntityFrameworkCore.Migrations;

namespace KonusarakOgrenQuiz.Migrations
{
    public partial class wiredTexts : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "wiredId",
                table: "questions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Wired",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    titles = table.Column<string>(type: "TEXT", nullable: true),
                    smallText = table.Column<string>(type: "TEXT", nullable: true),
                    details = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wired", x => x.id);
                });

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_questions_Wired_wiredId",
                table: "questions");

            migrationBuilder.DropTable(
                name: "Wired");

            migrationBuilder.DropIndex(
                name: "IX_questions_wiredId",
                table: "questions");

            migrationBuilder.DropColumn(
                name: "wiredId",
                table: "questions");
        }
    }
}
