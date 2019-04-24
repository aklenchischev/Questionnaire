using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Questionnaire.Infrastructure.QuestionnaireMigrations
{
    public partial class AddPollTimestamp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Timestamp",
                table: "Polls",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Timestamp",
                table: "Polls");
        }
    }
}
