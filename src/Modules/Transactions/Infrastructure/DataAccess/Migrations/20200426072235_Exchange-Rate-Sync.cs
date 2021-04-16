using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class ExchangeRateSync : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ExternalId",
                table: "ExchangeRate",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "ExchangeRate",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "ModifiedOn",
                table: "ExchangeRate",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExchangeRate",
                nullable: true);

            migrationBuilder.Sql("UPDATE [ExchangeRate] SET [ExternalId] = NEWID ( )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExternalId",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "ModifiedOn",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExchangeRate");
        }
    }
}
