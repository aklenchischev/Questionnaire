using Microsoft.EntityFrameworkCore.Migrations;

namespace Questionnaire.Infrastructure.QuestionnaireMigrations
{
    public partial class AddNotAnsweredQuestionsCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NotAnsweredQuestionsCount",
                table: "Polls",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NotAnsweredQuestionsCount",
                table: "Polls");
        }
    }
}
