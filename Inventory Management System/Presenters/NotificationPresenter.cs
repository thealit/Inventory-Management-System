using Inventory_Management_System.Components;
using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    
    public class NotificationPresenter 
    {
        private readonly INotificationRepository _repository;
        private readonly IInventoryRepository _inventoryRepository;
        private readonly InventoryForm _inventoryForm;
        private InventoryPresenter _inventoryPresenter;

        public NotificationPresenter(InventoryForm inventoryForm, INotificationRepository repository, IInventoryRepository inventoryRepository)
        {
            _repository = repository;
            _inventoryForm = inventoryForm;
            _inventoryRepository = inventoryRepository;

            _inventoryForm.NotifPanel.MarkAllAsReadClicked += OnMarkAllAsRead_Clicked;
            _inventoryForm.NotifPanel.ViewAllNotifClicked += OnViewAllNotif_Clicked;
            _inventoryForm.NotifPanel.ViewUnreadClicked += OnViewAllUnreadNotif_Clicked;
            _inventoryForm.NotifPanel.OnClearAll += OnClearAll_Clicked;
            _inventoryForm.NotifPanel.NotifMenuActionClicked += OnNotifMenuAction;
            _inventoryForm.NotifPanel.RefreshNotifs += (s, e) => _ = LoadNotificationsAsync();
            _inventoryForm.NotifPanel.OnClearPast30DaysNotifs += OnClearPast30DaysNotifs_Clicked;

            PublicEvents.InventoryChanged += OnInventoryChanged;
            PublicEvents.UpdateNotif += OnInventoryChanged;
        }


        public async void OnInventoryChanged(object? sender, EventArgs e)
        {
            await LoadNotificationsAsync();
        }

        private void OnNotifMenuAction(string action)
        {
            switch (action)
            {
                case "view_all":
                    OnViewAllNotif_Clicked(this, EventArgs.Empty);
                    break;
                case "view_unread":
                    OnViewAllUnreadNotif_Clicked(this, EventArgs.Empty);
                    break;
                case "mark_all_read":
                    OnMarkAllAsRead_Clicked(this, EventArgs.Empty);
                    break;
                case "clear_all":
                    OnClearAll_Clicked(this, EventArgs.Empty);
                    break;
                case "clear_past30days":
                    OnClearPast30DaysNotifs_Clicked(this, EventArgs.Empty);
                    break;
            }
        }


        


        // status: 1-> all, 2->unread
        public async Task LoadNotificationsAsync()
        {
            await GenerateStockAlertsAsync();

            int userId = CurrentUser.UserID;

            if (userId == 0) return;
            
            var notifs = await _repository.GetAllNotificationsAsync(userId);

            if (!_inventoryForm.IsHandleCreated) return;

            _inventoryForm.Invoke(new Action(() =>
            {
                _inventoryForm.NotifPanel.SuspendLayout();

                _inventoryForm.NotifPanel.ClearNotifications();

                _inventoryForm.NotifPanel.BeginLoadNotifications();

                if (notifs != null && notifs.Count > 0)
                {
                    foreach (var item in notifs)
                    {

                        _inventoryForm.NotifPanel.AddNotification(item);
                    }
                }
                else
                {
                    _inventoryForm.NotifPanel.DisplayNoNotif("No notifications found");
                }

                _inventoryForm.NotifPanel.EndLoadNotifications();

                bool hasUnread = notifs.Any(n => n.IsRead == false);
                _inventoryForm.ShowNotifRedDot(hasUnread);

                _inventoryForm.NotifPanel.ResumeLayout(true);
            }));
        }
        private async Task GenerateStockAlertsAsync()
        {
            var outOfStock = await _inventoryRepository.GetOutOfStockAsync();
            var lowStock = await _inventoryRepository.GetLowStockAsync();

            await _repository.GenerateStockNotificationsAsync(outOfStock, 0);
            await _repository.GenerateStockNotificationsAsync(lowStock, 1);
        }
        
        
       

        // notif menu buttons
        private int _notifMenuFilter = 0; // 0-> all, 1->unread, 2->clear all, 3->mark all read, 4->clear past 30 days
        private async void OnViewAllNotif_Clicked(object? sender, EventArgs e)
        {
            if(_notifMenuFilter == 0) return;
            _notifMenuFilter = 0;
            await LoadNotificationsAsync();
        }

        private async void OnViewAllUnreadNotif_Clicked(object? sender, EventArgs e)
        {
            if(_notifMenuFilter == 1) return;
            _notifMenuFilter = 1;
            var unreadNotifs = await _repository.UnreadNotificationsAsync(CurrentUser.UserID);
            _inventoryForm.Invoke(new Action(() =>
            {
                _inventoryForm.NotifPanel.SuspendLayout();
                _inventoryForm.NotifPanel.ClearNotifications();

                if (unreadNotifs != null && unreadNotifs.Count > 0)
                {
                    foreach (var item in unreadNotifs)
                    {
                        _inventoryForm.NotifPanel.AddNotification(item);
                    }
                }
                else
                {
                    _inventoryForm.NotifPanel.DisplayNoNotif("No unread notifications found");
                }

                _inventoryForm.NotifPanel.ResumeLayout(true);
            }));
        }
        private async void OnClearAll_Clicked(object? sender, EventArgs e)
        {
            int toDeleteCount = await _repository.GetToDeleteCountAsync(CurrentUser.UserID, past30Days: false);

            if (toDeleteCount == 0 || _notifMenuFilter == 2) return;
            _notifMenuFilter = 2;

            await _repository.Clear(CurrentUser.UserID, past30Days: false); // delete all
            await LoadNotificationsAsync();
        }
        private async void OnMarkAllAsRead_Clicked(object? sender, EventArgs e)
        {
            int toMarkAsReadCount = await _repository.GetUnreadCountAsync(CurrentUser.UserID);

            if (toMarkAsReadCount == 0 || _notifMenuFilter == 3) return;
            _notifMenuFilter = 3;

            await _repository.MarkAllRead(CurrentUser.UserID);
            await LoadNotificationsAsync();
        }
        private async void OnClearPast30DaysNotifs_Clicked(object? sender, EventArgs e)
        { 

             int toDeleteCount = await _repository.GetToDeleteCountAsync(CurrentUser.UserID, past30Days: true);

            if (toDeleteCount == 0 || _notifMenuFilter == 4) return;
            _notifMenuFilter = 4;

            await _repository.Clear(CurrentUser.UserID, past30Days: true);
            await LoadNotificationsAsync();
        }

    }
}
