using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class Files : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false),
                    CreatedOn = table.Column<DateTime>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ExternalId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FileToTransaction",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    FileId = table.Column<Guid>(nullable: false),
                    TransactionId = table.Column<int>(nullable: false),
                    ModifiedOn = table.Column<DateTime>(nullable: true),
                    ExternalId = table.Column<Guid>(nullable: false),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileToTransaction", x => x.Id);
                    table.ForeignKey(
                        name: "ForeignKey_FileToTransaction_File",
                        column: x => x.FileId,
                        principalTable: "File",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "ForeignKey_FileToTransaction_Transaction",
                        column: x => x.TransactionId,
                        principalTable: "Transaction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FileToTransaction_FileId",
                table: "FileToTransaction",
                column: "FileId");

            migrationBuilder.CreateIndex(
                name: "IX_FileToTransaction_TransactionId",
                table: "FileToTransaction",
                column: "TransactionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FileToTransaction");

            migrationBuilder.DropTable(
                name: "File");
        }
    }
}
