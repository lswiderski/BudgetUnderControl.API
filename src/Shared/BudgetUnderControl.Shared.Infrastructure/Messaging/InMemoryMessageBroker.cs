using System.Linq;
using System.Threading.Tasks;
using BudgetUnderControl.Shared.Abstractions.Messaging;
using BudgetUnderControl.Shared.Abstractions.Modules;

namespace BudgetUnderControl.Shared.Infrastructure.Messaging
{
    internal sealed class InMemoryMessageBroker : IMessageBroker
    {
        private readonly IModuleClient _moduleClient;
        private readonly IAsynchronousDispatcher _asynchronousDispatcher;

        public InMemoryMessageBroker(IModuleClient moduleClient, 
            IAsynchronousDispatcher asynchronousDispatcher)
        {
            _moduleClient = moduleClient;
            _asynchronousDispatcher = asynchronousDispatcher;
        }

        public async Task PublishAsync(params IMessage[] messages)
        {
            if (messages is null)
            {
                return;
            }

            messages = messages.Where(x => x is not null).ToArray();

            if (!messages.Any())
            {
                return;
            }

            var tasks = messages.Select(x => _asynchronousDispatcher.PublishAsync(x));

            await Task.WhenAll(tasks);
        }
    }
}