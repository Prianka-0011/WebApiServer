using Microsoft.EntityFrameworkCore.Migrations;

namespace Api_AngularFinal.Migrations
{
    public partial class scriptF : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TodoTask",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Place",
                table: "TodoTask",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TodoTask");

            migrationBuilder.DropColumn(
                name: "Place",
                table: "TodoTask");
        }
    }
}
