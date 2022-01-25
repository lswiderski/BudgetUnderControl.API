using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Modules.Transactions.Infrastructure.DataAccess.Migrations
{
    public partial class SeedInitialData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                           "VALUES ('PLN', 'Polski Złoty',985, 'zł' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('USD', 'United States dollar',840, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('EUR', 'Euro',978, '€' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('THB', 'Thai baht',764, '฿' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('CNY', 'Renminbi Juan',156, '¥' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('HKD', 'Hong Kong dollar',344, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('MOP', 'Macanese pataca',446, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('DKK', 'Danish krone',208, 'kr' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('ISK', 'Icelandic króna',352, 'kr' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('SEK', 'Swedish krona',752, 'kr' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('NOK', 'Norwegian krone',578, 'kr' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('UAH', 'Ukrainian hryvnia',980, '₴' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('BYN', 'Belarusian ruble',933, 'Br' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('RUB', 'Russian ruble',643, '₽' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('BRL', 'Brazilian real',986, 'R$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('PEN', 'Peruvian Sol',604, 'S' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('ARS', 'Argentine peso',032, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('MXN', 'Mexican peso',484, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('GBP', 'Pound sterling',826, '£' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('CHF', 'Swiss franc',756, 'Fr' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('CZK', 'Czech koruna',203, 'Kč' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('JPY', 'Japanese yen',392, '¥' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('KPW', 'North Korean won',408, '₩' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('KRW', 'South Korean won',410, '₩' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('ILS', 'Israeli new shekel',376, '₪' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('INR', 'Indian rupee',356, '₪' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('JOD', 'Jordanian dinar',400, 'JD' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('PHP', 'Philippine piso',608, '₱' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('AUD', 'Australian dollar','036', '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('CAD', 'Canadian dollar',124, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('AED', 'United Arab Emirates dirham',784, 'د.إ' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('HUF', 'Hungarian forint',348, 'Ft' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                                "VALUES ('RON', 'Romanian leu',946, 'lei' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                       "VALUES ('CUP', 'Cuban Peso',192, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('CUC', 'Cuban convertible peso',931, '$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('IDR', 'Indonesian rupiah',360, 'Rp' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('MYR', 'Malaysian ringgit',458, 'RM' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('SGD', 'Singapore dollar',702, 'S$' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('MMK', 'Myanmar kyat',104, 'K' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('VND', 'Vietnamese đồng',704, 'd' )");
            migrationBuilder.Sql("INSERT INTO transactions.\"Currency\" (\"Code\",\"FullName\",\"Number\",\"Symbol\")" +
                                              "VALUES ('BOB', 'Boliviano',068, 'Bs' )");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
