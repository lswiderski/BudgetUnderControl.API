using System.Threading.Tasks;
using BudgetUnderControl.Shared.Abstractions.Messaging;

namespace BudgetUnderControl.Shared.Infrastructure.Messaging
{
    public interface IAsynchronousDispatcher
    {
        Task PublishAsync<TMessage>(TMessage message) where TMessage : class, IMessage;
    }
}