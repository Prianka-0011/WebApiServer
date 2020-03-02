using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class ScriptFini : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GoingEvents",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    GoingTaskId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false)
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
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotInterests", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GoingEvents");

            migrationBuilder.DropTable(
                name: "MayBeGoingEs");

            migrationBuilder.DropTable(
                name: "NotInterests");
        }
    }
}
