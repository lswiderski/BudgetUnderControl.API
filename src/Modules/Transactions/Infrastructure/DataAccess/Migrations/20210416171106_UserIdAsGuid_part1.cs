using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess.Migrations
{
    public partial class UserIdAsGuid_part1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Tag",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@" update [dbo].[Tag]
                                    set [UserId] = [dbo].[User].[ExternalId]
                                    from [dbo].[User]
                                    where [dbo].[Tag].[OwnerId] = [dbo].[User].[Id]");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Synchronization",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@" update [dbo].[Synchronization]
                                    set [OwnerId] = [dbo].[User].[ExternalId]
                                    from [dbo].[User]
                                    where [dbo].[Synchronization].[UserId] = [dbo].[User].[Id]");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "ExchangeRate",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.Sql(@" update [dbo].[ExchangeRate]
                                    set [OwnerId] = [dbo].[User].[ExternalId]
                                    from [dbo].[User]
                                    where [dbo].[ExchangeRate].[UserId] IS NOT NULL AND [dbo].[ExchangeRate].[UserId] = [dbo].[User].[Id] ");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Category",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@" update [dbo].[Category]
                                    set [UserId] = [dbo].[User].[ExternalId]
                                    from [dbo].[User]
                                    where [dbo].[Category].[OwnerId] = [dbo].[User].[Id]");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "AccountGroup",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@" update [dbo].[AccountGroup]
                                    set [UserId] = [dbo].[User].[ExternalId]
                                    from [dbo].[User]
                                    where [dbo].[AccountGroup].[OwnerId] = [dbo].[User].[Id]");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Account",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));


            migrationBuilder.Sql(@" update [dbo].[Account]
                                    set [UserId] = [dbo].[User].[ExternalId]
                                    from [dbo].[User]
                                    where [dbo].[Account].[OwnerId] = [dbo].[User].[Id]");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Synchronization");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "ExchangeRate");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "AccountGroup");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Account");
        }
    }
}
