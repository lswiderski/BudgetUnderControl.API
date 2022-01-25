using System.Threading;
using System.Threading.Tasks;
using BudgetUnderControl.Modules.Notifications.Core.Services.NotificationsService;
using BudgetUnderControl.Shared.Application.CQRS.Configuration.Commands;
using MediatR;

namespace BudgetUnderControl.Modules.Notifications.Core.Commands.ActivateUserNotification
{
    public class CreateNotificationCommandHandler : ICommandHandler<CreateActivateUserNotificationCommand>
    {
        private readonly INotificationsService _notificationService;

        internal CreateNotificationCommandHandler(INotificationsService notificationService)
        {
            this._notificationService = notificationService;
        }

        public async Task<Unit> Handle(CreateActivateUserNotificationCommand request, CancellationToken cancellationToken)
        {
            await this._notificationService.SendRegisterNotificationAsync(request.ReceiverEmail, request.ReceiverFirstName, request.ReceiverLastName, request.ActivationCode);

            return Unit.Value;
        }
    }
}