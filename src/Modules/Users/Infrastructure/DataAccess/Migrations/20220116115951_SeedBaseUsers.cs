using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Modules.Users.Infrastructure.DataAccess.Migrations
{
    public partial class SeedBaseUsers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO users.\"User\" (\"Id\",\"Username\",\"FirstName\",\"LastName\",\"Role\",\"Email\",\"Password\",\"Salt\",\"CreatedAt\", \"IsDeleted\", \"IsActivated\", \"ActivatedOn\")" +
                                "VALUES ('10000000-0000-0000-0000-000000000001', 'demo', 'demo','demo', 'User', 'demo@swiderski.xyz','/YbxhIqptH2KBOvB56OEtWBaD/o=', 's0mRIdlKvI','2018-11-11 15:29:09.584149', false, true, '2021-04-15 18:02:09.584149')");

            migrationBuilder.Sql("INSERT INTO users.\"User\" (\"Id\",\"Username\",\"FirstName\",\"LastName\",\"Role\",\"Email\",\"Password\",\"Salt\",\"CreatedAt\", \"IsDeleted\", \"IsActivated\", \"ActivatedOn\")" +
                                "VALUES ('10000001-1001-1001-1001-100000000001', 'bucadmin', 'admin','admin', 'Admin', 'bucadmin@swiderski.xyz','/YbxhIqptH2KBOvB56OEtWBaD/o=', 's0mRIdlKvI','2018-11-11 15:29:09.584149', false, true, '2021-04-15 18:02:09.584149')"); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
