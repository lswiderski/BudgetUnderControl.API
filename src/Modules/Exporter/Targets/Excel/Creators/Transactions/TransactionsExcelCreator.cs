using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Modules.Exporter.Core.Services;
using ClosedXML.Excel;

namespace BudgetUnderControl.Modules.Exporter.Targets.Excel.Builders.Transactions
{
    public class TransactionsExcelCreator :  ITransacationsReportCreator
    {
        public async Task<TransactionsReport> CreateReportAsync(ICollection<TransactionDTO> transactions)
        {
            await Task.CompletedTask;
            
            using (var workbook = new XLWorkbook())
            {
                int row = 1;
                IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Transactions");
                worksheet.Cell(row , 1).Value = "Transaction Id";
                worksheet.Cell(row , 2).Value = "Is Transfer";
                worksheet.Cell(row , 3).Value = "Date";
                worksheet.Cell(row , 4).Value = "Times";
                worksheet.Cell(row , 5).Value = "Name";
                worksheet.Cell(row , 6).Value = "Amount";
                worksheet.Cell(row , 7).Value = "Currency Code";
                worksheet.Cell(row , 8).Value = "Category";
                worksheet.Cell(row , 9).Value = "Type";
                worksheet.Cell(row , 10).Value = "Account Name";
                worksheet.Cell(row , 11).Value = "Tags";
                for (int index = 1; index <= 11; index++)
                {
                    worksheet.Cell(row , index).Style.Font.Bold = true;
                }

                
                foreach (var transaction in transactions)
                {
                    row++;
                    worksheet.Cell(row, 1).Value = transaction.Id;
                    worksheet.Cell(row, 2).Value = transaction.IsTransfer != null && transaction.IsTransfer.Value ? 1 : 0;
                    worksheet.Cell(row, 3).Value = transaction.Date.ToLocalTime().ToString("dd/MM/yyyy");
                    worksheet.Cell(row, 4).Value = transaction.Date.ToLocalTime().ToString("HH:mm");
                    worksheet.Cell(row, 5).Value = transaction.Name;
                    worksheet.Cell(row, 6).Value = transaction.Value;
                    worksheet.Cell(row, 7).Value = transaction.CurrencyCode;
                    worksheet.Cell(row, 8).Value = transaction.Category;
                    worksheet.Cell(row, 9).Value = transaction.Type.ToString();
                    worksheet.Cell(row, 10).Value = transaction.Account;
                    worksheet.Cell(row, 11).Value = transaction.TagsJoined;
                }
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    var content = stream.ToArray();
                    return new TransactionsReport
                    {
                        Content = content,
                        ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        Name = "Report.xlsx",
                    };
                }
            }
        }
    }
}