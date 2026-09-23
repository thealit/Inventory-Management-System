using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface INotificationViews
    {
        event EventHandler? MarkAllAsReadClicked;
        event EventHandler? ViewAllNotifClicked;
        event EventHandler? ViewUnreadClicked;
        event EventHandler? NotifMenuClicked;
        event Action<string>? NotifMenuActionClicked;
        event EventHandler? OnClearAll;
        event EventHandler? OnClearPast30DaysNotifs;

        void ClearNotifications();
        void DisplayNoNotif(string customText);
        void AddNotification(Models.NotificationModel model);
        void BeginLoadNotifications();
        void EndLoadNotifications();
        
    }
}
