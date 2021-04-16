using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class FixTransfersExternalIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("update [Transaction] set [ExternalId] = NEWID ( ) FROM [Transaction] t LEFT OUTER JOIN [Transfer] ft on ft.FromTransactionId = t.Id LEFT OUTER JOIN [Transfer] tt on tt.ToTransactionId = t.Id where tt.[ToTransactionId] is not null or ft.[FromTransactionId] is not null");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
