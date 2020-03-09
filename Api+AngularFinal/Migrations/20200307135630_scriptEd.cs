using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class scriptEd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventsGoPeople",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    GMN = table.Column<int>(nullable: false),
                    TaskId = table.Column<Guid>(nullable: false),
                    TodoTaskId = table.Column<Guid>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventsGoPeople", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventsGoPeople_TodoTask_TodoTaskId",
                        column: x => x.TodoTaskId,
                        principalTable: "TodoTask",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventsGoPeople_TodoTaskId",
                table: "EventsGoPeople",
                column: "TodoTaskId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventsGoPeople");
        }
    }
}
