﻿using System.Threading.Channels;
using BudgetUnderControl.Shared.Abstractions.Messaging;

namespace BudgetUnderControl.Shared.Infrastructure.Messaging
{
    internal sealed class MessageChannel : IMessageChannel
    {
        private readonly Channel<IMessage> _messages = Channel.CreateUnbounded<IMessage>();

        public ChannelReader<IMessage> Reader => _messages.Reader;
        public ChannelWriter<IMessage> Writer => _messages.Writer;
    }
}