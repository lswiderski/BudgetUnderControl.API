using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class SeedSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {


            migrationBuilder.Sql("INSERT INTO [USER] (Username,Role,Email,Password,Salt,CreatedAt,ExternalId)" +
                                               "VALUES ('demo', 'User', 'demo@swiderski.xyz','11a78a6a43d03d06489bf0611735acd0', 's0mRIdlKvI','2018-11-11 15:29:09.584149','10000000-0000-0000-0000-000000000001')"); //password:asdfg


            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                            "VALUES ('PLN', 'Polski Złoty',985, 'zł' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('USD', 'United States dollar',840, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('EUR', 'Euro',978, '€' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('THB', 'Thai baht',764, '฿' )");

            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Food', 'cb80cf16-02db-47ba-9ee7-5d9e78a695db', 1)");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Transport', '3a458c42-f302-4664-86e5-7ac9598cd511', 1 )");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Other' , 'e457e4d5-83f5-481e-a34d-6b983fb37756', 1)");

            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Salary', '153253bd-a5e6-47b6-889f-cfcd3146cbae', 1 )");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Taxes' , '1d268617-00a4-458b-a356-b86cc9aed482', 1)");
            migrationBuilder.Sql("INSERT INTO ACCOUNTGROUP (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Cash' , '5f9c5721-24e8-4366-8e81-bbb09b20f265', 1)");
            migrationBuilder.Sql("INSERT INTO ACCOUNTGROUP (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Account', '34a216a1-c9cb-4012-9344-8c39ff5d5b4c', 1 )");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                               "VALUES ('Entertainment' , '4313c995-993a-4e16-9a7e-f0e5120917de', 1)");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Health' , '28f813d3-1d26-44c1-a3b4-88b17fb9963c', 1)");
            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                                "VALUES ('Interest' , 'ddef31c7-7fa4-4e0c-a456-55b85353b46d', 1)");

            migrationBuilder.Sql("INSERT INTO CATEGORY (Name, ExternalId, OwnerId)" +
                                              "VALUES ('Home' , '3956d5ea-a428-453b-9f68-cf486dd942ff', 1)");

            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('CNY', 'Renminbi Juan',156, '¥' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('HKD', 'Hong Kong dollar',344, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('MOP', 'Macanese pataca',446, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('DKK', 'Danish krone',208, 'kr' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('ISK', 'Icelandic króna',352, 'kr' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('SEK', 'Swedish krona',752, 'kr' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('NOK', 'Norwegian krone',578, 'kr' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('UAH', 'Ukrainian hryvnia',980, '₴' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('BYN', 'Belarusian ruble',933, 'Br' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('RUB', 'Russian ruble',643, '₽' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('BRL', 'Brazilian real',986, 'R$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('PEN', 'Peruvian Sol',604, 'S' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('ARS', 'Argentine peso',032, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('MXN', 'Mexican peso',484, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('GBP', 'Pound sterling',826, '£' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('CHF', 'Swiss franc',756, 'Fr' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('CZK', 'Czech koruna',203, 'Kč' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('JPY', 'Japanese yen',392, '¥' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('KPW', 'North Korean won',408, '₩' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('KRW', 'South Korean won',410, '₩' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('ILS', 'Israeli new shekel',376, '₪' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('INR', 'Indian rupee',356, '₪' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('JOD', 'Jordanian dinar',400, 'JD' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('PHP', 'Philippine piso',608, '₱' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('AUD', 'Australian dollar','036', '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('CAD', 'Canadian dollar',124, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('AED', 'United Arab Emirates dirham',784, 'د.إ' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('HUF', 'Hungarian forint',348, 'Ft' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                                "VALUES ('RON', 'Romanian leu',946, 'lei' )");
   
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
