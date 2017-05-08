using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestASP.NetIdentity.DataAccess.Migrations
{
    public partial class Migration002 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questionnaire_QuestionId",
                schema: "dbo",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "QuestionId",
                schema: "dbo",
                table: "Answer",
                newName: "QuestionnaireId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionId",
                schema: "dbo",
                table: "Answer",
                newName: "IX_Answer_QuestionnaireId");

            migrationBuilder.AddColumn<int>(
                name: "NextQuestionId",
                schema: "dbo",
                table: "Answer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questionnaire_QuestionnaireId",
                schema: "dbo",
                table: "Answer",
                column: "QuestionnaireId",
                principalSchema: "dbo",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questionnaire_QuestionnaireId",
                schema: "dbo",
                table: "Answer");

            migrationBuilder.DropColumn(
                name: "NextQuestionId",
                schema: "dbo",
                table: "Answer");

            migrationBuilder.RenameColumn(
                name: "QuestionnaireId",
                schema: "dbo",
                table: "Answer",
                newName: "QuestionId");

            migrationBuilder.RenameIndex(
                name: "IX_Answer_QuestionnaireId",
                schema: "dbo",
                table: "Answer",
                newName: "IX_Answer_QuestionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answer_Questionnaire_QuestionId",
                schema: "dbo",
                table: "Answer",
                column: "QuestionId",
                principalSchema: "dbo",
                principalTable: "Questionnaire",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
