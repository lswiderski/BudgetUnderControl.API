using System;
using BudgetUnderControl.Shared.Abstractions.Events;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Events
{
    internal record UserActivationCodeResetEvent(Guid ReceiverId, string ReceiverFirstName, string ReceiverLastName,
        string ReceiverEmail, string ActivationCode) : IEvent;
}