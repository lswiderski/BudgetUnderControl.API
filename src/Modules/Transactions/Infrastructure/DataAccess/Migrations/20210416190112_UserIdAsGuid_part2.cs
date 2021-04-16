using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess.Migrations
{
    public partial class UserIdAsGuid_part2 : Migration
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
                name: "FK_Category_User_OwnerId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Synchronization_User_UserId",
                table: "Synchronization");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_OwnerId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "ForeignKey_Transaction_User",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "ExchangeRate");

            migrationBuilder.AlterColumn<int>(
                name: "AddedById",
                table: "Transaction",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<Guid>(
                name: "CreatedById",
                table: "Transaction",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.Sql(@" update [dbo].[Transaction]
                                    set [CreatedById] = [dbo].[Account].[UserId]
                                    from [dbo].[Account]
                                    where [dbo].[Transaction].[AccountId] = [dbo].[Account].[Id]");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Tag",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Synchronization",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Category",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "AccountGroup",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Account",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Account_User_OwnerId",
                table: "Account",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_AccountGroup_User_OwnerId",
                table: "AccountGroup",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Category_User_OwnerId",
                table: "Category",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Synchronization_User_UserId",
                table: "Synchronization",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_OwnerId",
                table: "Tag",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_AddedById",
                table: "Transaction",
                column: "AddedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Account_User_OwnerId",
                table: "Account");

            migrationBuilder.DropForeignKey(
                name: "FK_AccountGroup_User_OwnerId",
                table: "AccountGroup");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_User_OwnerId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Synchronization_User_UserId",
                table: "Synchronization");

            migrationBuilder.DropForeignKey(
                name: "FK_Tag_User_OwnerId",
                table: "Tag");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_AddedById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Transaction");

            migrationBuilder.AlterColumn<int>(
                name: "AddedById",
                table: "Transaction",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Tag",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "Synchronization",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "ExchangeRate",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Category",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "AccountGroup",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OwnerId",
                table: "Account",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

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
                name: "FK_Category_User_OwnerId",
                table: "Category",
                column: "OwnerId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Synchronization_User_UserId",
                table: "Synchronization",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Tag_User_OwnerId",
                table: "Tag",
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
                onDelete: ReferentialAction.Restrict);
        }
    }
}
