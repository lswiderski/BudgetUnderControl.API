using System.Threading.Tasks;
using BudgetUnderControl.Shared.Abstractions.Messaging;

namespace BudgetUnderControl.Shared.Infrastructure.Messaging
{
    internal sealed class AsynchronousDispatcher : IAsynchronousDispatcher
    {
        private readonly IMessageChannel _messageChannel;

        public AsynchronousDispatcher(IMessageChannel messageChannel)
            => _messageChannel = messageChannel;

        public async Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage
            => await _messageChannel.Writer.WriteAsync(message);
    }
}