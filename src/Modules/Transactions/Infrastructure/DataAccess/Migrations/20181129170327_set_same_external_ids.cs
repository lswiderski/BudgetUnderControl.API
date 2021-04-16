using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class set_same_external_ids : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = 'cb80cf16-02db-47ba-9ee7-5d9e78a695db' WHERE [Name] = 'Food'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = '3a458c42-f302-4664-86e5-7ac9598cd511' WHERE [Name] = 'Transport'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = 'e457e4d5-83f5-481e-a34d-6b983fb37756' WHERE [Name] = 'Other'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = '153253bd-a5e6-47b6-889f-cfcd3146cbae' WHERE [Name] = 'Salary'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = '1d268617-00a4-458b-a356-b86cc9aed482' WHERE [Name] = 'Taxes'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = '4313c995-993a-4e16-9a7e-f0e5120917de' WHERE [Name] = 'Entertainment'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = '28f813d3-1d26-44c1-a3b4-88b17fb9963c' WHERE [Name] = 'Health'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = 'ddef31c7-7fa4-4e0c-a456-55b85353b46d' WHERE [Name] = 'Interest'");
            migrationBuilder.Sql("UPDATE [Category] SET [ExternalId] = '3956d5ea-a428-453b-9f68-cf486dd942ff' WHERE [Name] = 'Home'");

            migrationBuilder.Sql("UPDATE [ACCOUNTGROUP] SET [ExternalId] = '5f9c5721-24e8-4366-8e81-bbb09b20f265' WHERE [Name] = 'Cash'");
            migrationBuilder.Sql("UPDATE [ACCOUNTGROUP] SET [ExternalId] = '34a216a1-c9cb-4012-9344-8c39ff5d5b4c' WHERE [Name] = 'Account'");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
