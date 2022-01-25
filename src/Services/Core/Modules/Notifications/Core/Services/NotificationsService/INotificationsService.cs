using System.Threading.Tasks;

namespace BudgetUnderControl.Modules.Notifications.Core.Services.NotificationsService
{
    public interface INotificationsService
    {
       Task SendRegisterNotificationAsync(string emailAddress, string firstName, string lastName, string code);
    }
}
