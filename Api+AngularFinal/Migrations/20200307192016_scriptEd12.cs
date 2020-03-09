using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class scriptEd12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EventsGoPeople_TodoTask_TodoTaskId",
                table: "EventsGoPeople");

            migrationBuilder.DropTable(
                name: "GoingEvents");

            migrationBuilder.DropTable(
                name: "MayBeGoingEs");

            migrationBuilder.DropTable(
                name: "NotInterests");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EventsGoPeople",
                table: "EventsGoPeople");

            migrationBuilder.RenameTable(
                name: "EventsGoPeople",
                newName: "eventsGoPeoples");

            migrationBuilder.RenameIndex(
                name: "IX_EventsGoPeople_TodoTaskId",
                table: "eventsGoPeoples",
                newName: "IX_eventsGoPeoples_TodoTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_eventsGoPeoples",
                table: "eventsGoPeoples",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_eventsGoPeoples_TodoTask_TodoTaskId",
                table: "eventsGoPeoples",
                column: "TodoTaskId",
                principalTable: "TodoTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_eventsGoPeoples_TodoTask_TodoTaskId",
                table: "eventsGoPeoples");

            migrationBuilder.DropPrimaryKey(
                name: "PK_eventsGoPeoples",
                table: "eventsGoPeoples");

            migrationBuilder.RenameTable(
                name: "eventsGoPeoples",
                newName: "EventsGoPeople");

            migrationBuilder.RenameIndex(
                name: "IX_eventsGoPeoples_TodoTaskId",
                table: "EventsGoPeople",
                newName: "IX_EventsGoPeople_TodoTaskId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EventsGoPeople",
                table: "EventsGoPeople",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "GoingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GoingTaskId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GoingEvents", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MayBeGoingEs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    MayBeTaskId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MayBeGoingEs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotInterests",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    NotTaskId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotInterests", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_EventsGoPeople_TodoTask_TodoTaskId",
                table: "EventsGoPeople",
                column: "TodoTaskId",
                principalTable: "TodoTask",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
