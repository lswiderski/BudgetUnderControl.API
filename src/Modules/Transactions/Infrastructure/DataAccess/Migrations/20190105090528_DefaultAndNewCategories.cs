using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class DefaultAndNewCategories : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "Category",
                nullable: false,
                defaultValue: false);

            migrationBuilder.Sql("UPDATE [Category] SET [IsDefault] = 1 WHERE [NAME] ='Other'");

            migrationBuilder.Sql("INSERT INTO CATEGORY (Name,OwnerId,ModifiedOn,ExternalId)" +
                                                "VALUES ('Beauty',(SELECT TOP(1) Id FROM [USER]),GETUTCDATE(),'cb80cf16-02db-12de-9ee7-5d9e78a695db')");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name,OwnerId,ModifiedOn,ExternalId)" +
                                               "VALUES ('Groceries',(SELECT TOP(1) Id FROM [USER]),GETUTCDATE(),'cb80cf16-02db-442a-9ee7-5d9e785df5db' )");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name,OwnerId,ModifiedOn,ExternalId)" +
                                               "VALUES ('Loans',(SELECT TOP(1) Id FROM [USER]),GETUTCDATE(),'cb23cf16-02db-cd5a-9ee7-5d912ed695db' )");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name,OwnerId,ModifiedOn,ExternalId)" +
                                               "VALUES ('Gift',(SELECT TOP(1) Id FROM [USER]),GETUTCDATE(),'cb80ca16-02db-d2e2-9ee7-5d9e78a695db' )");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "Category");
        }
    }
}
