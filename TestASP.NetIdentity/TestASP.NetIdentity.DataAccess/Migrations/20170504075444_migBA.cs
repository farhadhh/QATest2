using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TestASP.NetIdentity.DataAccess.Migrations
{
    public partial class migBA : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDependent",
                schema: "dbo",
                table: "Question",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDependent",
                schema: "dbo",
                table: "Question");
        }
    }
}
