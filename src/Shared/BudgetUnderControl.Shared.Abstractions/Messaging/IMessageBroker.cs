using System.Threading.Tasks;

namespace BudgetUnderControl.Shared.Abstractions.Messaging
{
    public interface IMessageBroker
    {
        Task PublishAsync(params IMessage[] messages);
    }
}