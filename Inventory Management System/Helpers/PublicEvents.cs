using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Helpers
{
    public static class PublicEvents
    {
        public static event EventHandler<UserDeletedEventArgs>? UserDeletedByAdmin;
        public static event EventHandler<UserRoleChangedEventArgs>? UserRoleChangedByAdmin;
        public static event EventHandler<PasswordChangedEventArgs>? UserPasswordChangedByAdmin;
        public static event EventHandler? InventoryChanged;
        public static event EventHandler? UpdateNotif;
        public static event EventHandler? UpdateInventoryLogs;
        public static event EventHandler? UpdateUserManagement;
        public static event EventHandler? UpdateUnit_Category;

        // reloads notifs n inventory tiles n table
        public static void InvokeInventoryChanged(object sender)
        => InventoryChanged?.Invoke(sender, EventArgs.Empty);

        public static void InvokeUserDeletedByAdmin(object sender, int deletedUserId)
        => UserDeletedByAdmin?.Invoke(sender, new UserDeletedEventArgs(deletedUserId));

        public static void InvokeUserRoleChangedByAdmin(object sender, int userId)
        => UserRoleChangedByAdmin?.Invoke(sender, new UserRoleChangedEventArgs (userId));

        public static void InvokeUpdateNotif(object sender)
        => UpdateNotif?.Invoke(sender, EventArgs.Empty);

        public static void InvokeUpdateInventoryLogs(object sender)
        => UpdateInventoryLogs?.Invoke(sender, EventArgs.Empty);

        // reflect changes from user's manage account to user management
        public static void InvokeUpdateUserManagement(object sender)
        => UpdateUserManagement?.Invoke(sender, EventArgs.Empty);

        public static void InvokeUpdateUnit_CategoryDropdowns(object sender)
        => UpdateUnit_Category?.Invoke(sender, EventArgs.Empty);


        public static void InvokeUserPasswordChangedByAdmin(object sender, int userId, string newHashedPassword)
        {
            UserPasswordChangedByAdmin?.Invoke(sender, new PasswordChangedEventArgs(userId, newHashedPassword));
        }
    }

    public class SelectedChangedEventArgs : EventArgs
    {
        public bool IsSelectedChangeByUser { get; }

        public SelectedChangedEventArgs(bool selectedChangeByUser)
        {
            IsSelectedChangeByUser = selectedChangeByUser;
        }
    }

    // reflect changes from user management to user's manage account 
    public class PasswordChangedEventArgs : EventArgs
    {
        public int UserId { get; }
        public string NewHashedPassword { get; }

        public PasswordChangedEventArgs(int userId, string newHashedPassword)
        {
            UserId = userId;
            NewHashedPassword = newHashedPassword;
        }
    }

    public class UserDeletedEventArgs : EventArgs 
    { 
        public int DeletedUserId { get; }
        public UserDeletedEventArgs(int deletedUserId)
        {
            DeletedUserId = deletedUserId;
        }
    }

    public class UserRoleChangedEventArgs : EventArgs
    {
        public int UserId { get; }
        public UserRoleChangedEventArgs(int userId)
        {
            UserId = userId;
        }
    }

}
