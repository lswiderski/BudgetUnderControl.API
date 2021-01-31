using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class tokens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivationCode",
                table: "User");

            migrationBuilder.CreateTable(
                name: "Token",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserExternalId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<int>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Code = table.Column<string>(nullable: true),
                    ValidUntil = table.Column<DateTime>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    IsValid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Token", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_Token_User",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Token_UserId",
                table: "Token",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Token");

            migrationBuilder.AddColumn<string>(
                name: "ActivationCode",
                table: "User",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }
    }
}
