using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class Script1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EventLists",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Task = table.Column<string>(nullable: true),
                    Date = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventLists", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventLists");
        }
    }
}
