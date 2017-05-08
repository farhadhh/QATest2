using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestASP.NetIdentity.DataAccess.Migrations
{
    public partial class Migration001 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Answer_QuestionId",
                schema: "dbo",
                table: "Answer",
                column: "QuestionId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answer_Questionnaire_QuestionId",
                schema: "dbo",
                table: "Answer");

            migrationBuilder.DropIndex(
                name: "IX_Answer_QuestionId",
                schema: "dbo",
                table: "Answer");
        }
    }
}
