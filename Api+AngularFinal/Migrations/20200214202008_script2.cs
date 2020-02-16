using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class script2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "EventLists",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "EventLists");
        }
    }
}
