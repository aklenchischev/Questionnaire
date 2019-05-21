using Microsoft.EntityFrameworkCore.Migrations;

namespace Questionnaire.Infrastructure.QuestionnaireMigrations
{
    public partial class DomainModelInitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Sections_SectionId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Polls_PollId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "Answer",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "QuestionBody",
                table: "Questions");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Polls");

            migrationBuilder.DropColumn(
                name: "NotAnsweredQuestionsCount",
                table: "Polls");

            migrationBuilder.AlterColumn<int>(
                name: "PollId",
                table: "Sections",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "SectionId",
                table: "Sections",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "Questions",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "QuestionId",
                table: "Questions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Sections_SectionId",
                table: "Questions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Polls_PollId",
                table: "Sections",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Sections_SectionId",
                table: "Questions");

            migrationBuilder.DropForeignKey(
                name: "FK_Sections_Polls_PollId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "SectionId",
                table: "Sections");

            migrationBuilder.DropColumn(
                name: "QuestionId",
                table: "Questions");

            migrationBuilder.AlterColumn<int>(
                name: "PollId",
                table: "Sections",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Sections",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "SectionId",
                table: "Questions",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Answer",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "QuestionBody",
                table: "Questions",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Polls",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NotAnsweredQuestionsCount",
                table: "Polls",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Sections_SectionId",
                table: "Questions",
                column: "SectionId",
                principalTable: "Sections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Sections_Polls_PollId",
                table: "Sections",
                column: "PollId",
                principalTable: "Polls",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
