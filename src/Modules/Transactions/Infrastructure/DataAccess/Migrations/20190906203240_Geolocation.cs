using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class Geolocation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Latitude",
                table: "Transaction",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "Longitude",
                table: "Transaction",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Latitude",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "Longitude",
                table: "Transaction");
        }
    }
}
