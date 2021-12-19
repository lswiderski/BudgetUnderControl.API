using System.Threading.Channels;
using BudgetUnderControl.Shared.Abstractions.Messaging;

namespace BudgetUnderControl.Shared.Infrastructure.Messaging
{
    public interface IMessageChannel
    {
        ChannelReader<IMessage> Reader { get; }
        ChannelWriter<IMessage> Writer { get; }
    }
}