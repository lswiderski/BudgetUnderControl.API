using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess.Migrations
{
    public partial class UserIdAsGuid_part3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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
                name: "ForeignKey_Token_User",
                table: "Token");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_User_AddedById",
                table: "Transaction");

            migrationBuilder.DropTable(
                name: "User");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_AddedById",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Token_UserId",
                table: "Token");

            migrationBuilder.DropIndex(
                name: "IX_Tag_OwnerId",
                table: "Tag");

            migrationBuilder.DropIndex(
                name: "IX_Synchronization_UserId",
                table: "Synchronization");

            migrationBuilder.DropIndex(
                name: "IX_Category_OwnerId",
                table: "Category");

            migrationBuilder.DropIndex(
                name: "IX_AccountGroup_OwnerId",
                table: "AccountGroup");

            migrationBuilder.DropIndex(
                name: "IX_Account_OwnerId",
                table: "Account");

            migrationBuilder.DropColumn(
                name: "AddedById",
                table: "Transaction");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Tag");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Synchronization");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Category");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "AccountGroup");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Account");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddedById",
                table: "Transaction",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Tag",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Synchronization",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Category",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "AccountGroup",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Account",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ActivatedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ExternalId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsActivated = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transaction_AddedById",
                table: "Transaction",
                column: "AddedById");

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tag_OwnerId",
                table: "Tag",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Synchronization_UserId",
                table: "Synchronization",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Category_OwnerId",
                table: "Category",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_AccountGroup_OwnerId",
                table: "AccountGroup",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Account_OwnerId",
                table: "Account",
                column: "OwnerId");

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
                name: "ForeignKey_Token_User",
                table: "Token",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_User_AddedById",
                table: "Transaction",
                column: "AddedById",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
