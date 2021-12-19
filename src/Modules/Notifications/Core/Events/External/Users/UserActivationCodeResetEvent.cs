using System;
using BudgetUnderControl.Shared.Abstractions.Events;

namespace BudgetUnderControl.Modules.Notifications.Core.Events.External.Users
{
    internal record UserActivationCodeResetEvent(Guid ReceiverId, string ReceiverFirstName, string ReceiverLastName,
        string ReceiverEmail, string ActivationCode) : IEvent;
}