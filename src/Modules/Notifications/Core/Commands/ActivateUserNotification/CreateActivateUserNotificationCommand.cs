using System;
using BudgetUnderControl.Shared.Application.CQRS.Contracts;

namespace BudgetUnderControl.Modules.Notifications.Core.Commands.ActivateUserNotification
{
    public class CreateActivateUserNotificationCommand : CommandBase
    {
        public Guid ReceiverId { get; set; }
        
        public string ReceiverFirstName { get; set; }
        
        public string ReceiverLastName { get; set; }
        
        public string ReceiverEmail { get; set; }
        
        public string ActivationCode { get; set; }
    }
}