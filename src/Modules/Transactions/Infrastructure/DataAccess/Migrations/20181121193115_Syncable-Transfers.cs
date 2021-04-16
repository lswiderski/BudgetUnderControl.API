using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class SyncableTransfers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "Transfer",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Transfer",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "Transfer",
                nullable: true);

            migrationBuilder.Sql("UPDATE [Transfer] SET [ExternalId] = NEWID ( )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Transfer");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "Transfer");
        }
    }
}
