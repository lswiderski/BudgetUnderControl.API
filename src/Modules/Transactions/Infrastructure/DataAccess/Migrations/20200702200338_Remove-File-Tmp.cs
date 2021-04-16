using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class RemoveFileTmp : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "File");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
