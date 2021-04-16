using Microsoft.EntityFrameworkCore.Migrations;

namespace BudgetUnderControl.Migrations.SqlServer.Migrations
{
    public partial class currencies : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                   "VALUES ('CUP', 'Cuban Peso',192, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('CUC', 'Cuban convertible peso',931, '$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('IDR', 'Indonesian rupiah',360, 'Rp' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('MYR', 'Malaysian ringgit',458, 'RM' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('SGD', 'Singapore dollar',702, 'S$' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('MMK', 'Myanmar kyat',104, 'K' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('VND', 'Vietnamese đồng',704, 'd' )");
            migrationBuilder.Sql("INSERT INTO CURRENCY (Code,FullName,Number,Symbol)" +
                                              "VALUES ('BOB', 'Boliviano',068, 'Bs' )");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
