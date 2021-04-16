using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class SynchronizationTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Account_User",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_AccountGroup_User",
                table: "AccountGroup");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Transaction_User",
                table: "Transaction");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "User",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Transaction",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Tag",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Category",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AccountGroup",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Account",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Synchronization",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LastSyncAt = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Component = table.Column<byte>(nullable: false),
                    ComponentId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Synchronization", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Synchronization_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Synchronization_UserId",
                table: "Synchronization",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Account_User",
                table: "Account",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_AccountGroup_User",
                table: "AccountGroup",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Transaction_User",
                table: "Transaction",
                column: "AddedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Account_User",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_AccountGroup_User",
                table: "AccountGroup");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Transaction_User",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "Synchronization");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "User");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AccountGroup");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "Account");

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Account_User",
                table: "Account",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_AccountGroup_User",
                table: "AccountGroup",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "ForeignKey_Transaction_User",
                table: "Transaction",
                column: "AddedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
