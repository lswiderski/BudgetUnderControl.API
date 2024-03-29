﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions.DTO;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Modules.Exporter.Core.Services;


namespace BudgetUnderControl.Modules.Exporter.Targets.CSV.Creators.Transactions
{
    public class TransactionsCSVCreator : ITransacationsReportCreator
    {
        public async Task<TransactionsReport> CreateReportAsync(ICollection<TransactionExportItemDto> transactions)
        {
            await Task.CompletedTask;
            var lines = new List<string>();
            var firstLine =
                "TransactionId;IsTransfer;Date;Time;Name;Amount;CurrencyCode;Category;Type;AccountName;Comments;Tags";
            lines.Add(firstLine);
            foreach (var item in transactions)
            {
                var line = string.Format("{0};{1};{2};{3};{4};{5};{6};{7};{8};{9};{10};{11}",
                    item.Id, item.IsTransfer.ToString(),
                    item.Date.ToLocalTime().ToString("dd/MM/yyyy"), item.Date.ToLocalTime().ToString("HH:mm"),
                    item.Name, item.Value, item.Currency, item.CategoryName, item.Type.ToString(), item.AccountName,
                    item.Comment, item.TagsJoined);
                lines.Add(line);
            }

            var stream = new MemoryStream();
            using (var sw = new StreamWriter(stream: stream))
            {
                foreach (string line in lines)
                {
                    sw.WriteLine(line);
                }
            }

            return new TransactionsReport
            {
                Content = stream.ToArray(),
                ContentType = "text/csv",
                Name = "Report.csv",
            };
        }
    }
}