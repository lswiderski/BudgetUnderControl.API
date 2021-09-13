using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Modules.Exporter.Core.Services;
using ClosedXML.Excel;

namespace BudgetUnderControl.Modules.Exporter.Targets.Excel.Builders.Transactions
{
    public class TransactionsExcelCreator : ITransacationsReportCreator
    {
        public async Task<TransactionsReport> CreateReportAsync(ICollection<TransactionExportItemDto> transactions)
        {
            await Task.CompletedTask;
            var columns = new SortedList()
            {
                { 1, "Transaction Id" },
                { 2, "Local Id" },
                { 3, "Date" },
                { 4, "Time" },
                { 5, "Name" },
                { 6, "Type" },
                { 7, "Amount" },
                { 8, "Currency" },
                { 9, "Amount in Main Currency" },
                { 10, "Category" },
                { 11, "Account" },
                { 12, "Is Transfer" },
                { 13, "Comment" },
                { 14, "Tags" },
            };

            using (var workbook = new XLWorkbook())
            {
                int row = 1;
                IXLWorksheet worksheet =
                    workbook.Worksheets.Add("Transactions");

                for (int index = 1; index <= columns.Count; index++)
                {
                    worksheet.Cell(row, index).Value = columns[index];
                    worksheet.Cell(row, index).Style.Font.Bold = true;
                }

                foreach (var transaction in transactions)
                {
                    row++;
                    var col = 1;
                    worksheet.Cell(row, col++).Value = transaction.TransactionId;
                    worksheet.Cell(row, col++).Value = transaction.Id;
                    worksheet.Cell(row, col++).Value = transaction.Date.ToLocalTime().ToString("dd/MM/yyyy");
                    worksheet.Cell(row, col++).Value = transaction.Date.ToLocalTime().ToString("HH:mm");
                    worksheet.Cell(row, col++).Value = transaction.Name;
                    worksheet.Cell(row, col++).Value = transaction.Type.ToString();
                    worksheet.Cell(row, col++).Value = transaction.Value;
                    worksheet.Cell(row, col++).Value = transaction.Currency;
                    worksheet.Cell(row, col++).Value = transaction.ValueInMainCurrency;
                    worksheet.Cell(row, col++).Value = transaction.CategoryName;
                    worksheet.Cell(row, col++).Value = transaction.AccountName;
                    worksheet.Cell(row, col++).Value = transaction.IsTransfer.ToString();
                    worksheet.Cell(row, col++).Value = transaction.Comment;
                    worksheet.Cell(row, col++).Value = transaction.TagsJoined;
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