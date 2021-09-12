using System;
using System.Threading;
using System.Threading.Tasks;
using Autofac;
using BudgetUnderControl.Modules.Exporter.Application.Configuration;
using BudgetUnderControl.Modules.Exporter.Core.Clients.Transactions;
using BudgetUnderControl.Modules.Exporter.Core.DTO;
using BudgetUnderControl.Modules.Exporter.Core.Services;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Queries;
using MediatR;

namespace BudgetUnderControl.Modules.Exporter.Application.Commands.Transactions
{
    internal class GetTransactionsQueryHandler : IQueryHandler<GetTransactionsQuery, TransactionsReport>
    {
        private readonly ITransactionsApiClient apiClient;

        public GetTransactionsQueryHandler(ITransactionsApiClient apiClient)
        {
            this.apiClient = apiClient;
        }

        public async Task<TransactionsReport> Handle(GetTransactionsQuery request,
            CancellationToken cancellationToken)
        {
            using (var scope = ExporterCompositionRoot.BeginLifetimeScope())
            {
                var creator = scope.ResolveKeyed<ITransacationsReportCreator>(request.Type);
                var result = await apiClient.GetTransactionsAsync(
                    new Core.Clients.Transactions.Requests.GetTransactionsQuery(request.Filters ??
                        new Core.Clients.Transactions.DTO.TransactionsFilterDTO()));
                var transactions = result.Transactions;
                var report = await creator.CreateReportAsync(transactions);

                return report;
            }
        }
    }
}