using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class Script4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "EventLists");

            migrationBuilder.DropColumn(
                name: "Task",
                table: "EventLists");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "EventLists",
                newName: "TaskId");

            migrationBuilder.AddColumn<Guid>(
                name: "TodoTaskId",
                table: "EventLists",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EventLists_TodoTaskId",
                table: "EventLists",
                column: "TodoTaskId");

            migrationBuilder.AddForeignKey(
                name: "FK_EventLists_TodoTask_TodoTaskId",
                table: "EventLists",
                column: "TodoTaskId",
                principalTable: "TodoTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventLists_TodoTask_TodoTaskId",
                table: "EventLists");

            migrationBuilder.DropIndex(
                name: "IX_EventLists_TodoTaskId",
                table: "EventLists");

            migrationBuilder.DropColumn(
                name: "TodoTaskId",
                table: "EventLists");

            migrationBuilder.RenameColumn(
                name: "TaskId",
                table: "EventLists",
                newName: "UserName");

            migrationBuilder.AddColumn<string>(
                name: "Date",
                table: "EventLists",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Task",
                table: "EventLists",
                nullable: true);
        }
    }
}
