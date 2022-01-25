using System;
using BudgetUnderControl.Shared.Abstractions.Events;

namespace BudgetUnderControl.Modules.Users.Infrastructure.Clients.Requests
{
    public record CreateActivateUserNotification(Guid ReceiverId, string ReceiverFirstName, string ReceiverLastName,
        string ReceiverEmail, string ActivationCode);

}