using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

/*
 * Updating of app infos thru DB polling [not exactly real-time]
 */

namespace Inventory_Management_System.Helpers
{
    public class CentralizedPoller
    {
        private System.Windows.Forms.Timer _timer;
        private readonly IChangeLogRepository _repo;
        private readonly int _currentUserId;
        private int _lastChangeId = 0;

        private static CentralizedPoller _instance;
        public static CentralizedPoller Instance => _instance;

        public static void Initialize(IChangeLogRepository repo, int currentUserId)
        {
            if (_instance != null) return; 
            _instance = new CentralizedPoller(repo, currentUserId);
        }

        private CentralizedPoller(IChangeLogRepository repo, int currentUserId)
        {
            _repo = repo;
            _currentUserId = currentUserId;
        }

        public async Task StartAsync(int intervalMs = 3000)
        {
            // Snapshot current latest ID on start
            // so old changes are never replayed
            _lastChangeId = await _repo.GetLatestChangeIdAsync();

            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = intervalMs;
            _timer.Tick += async (s, e) => await PollAsync();
            _timer.Start();
        }

        public void Stop() => _timer?.Stop();

        // Can be called manually to force immediate poll
        public async Task ForcePollAsync() => await PollAsync();
        public static void Clear() => _instance = null;

        private async Task PollAsync()
        {
            try
            {
                var changes = await _repo.GetChangesSinceAsync(_lastChangeId, _currentUserId);
                if (changes.Count == 0) return;

                _lastChangeId = changes.Max(c => c.ChangeId);

            
                // Marshal dispatching back to the UI thread
                if (System.Windows.Forms.Application.OpenForms.Count > 0)
                {
                    var mainForm = System.Windows.Forms.Application.OpenForms[0];
                    if (mainForm != null && mainForm.IsHandleCreated && !mainForm.IsDisposed)
                    {
                        mainForm.Invoke(() =>
                        {
                            foreach (var change in changes)
                                DispatchChange(change);
                        });
                    }
                }
            }
            catch (Exception)
            {}
        }

        private void DispatchChange(ChangeLogEntryModel change)
        {
            switch (change.ChangeType)
            {
                case ChangeTypes.UpdateAuditTrail:
                    PublicEvents.InvokeUpdateInventoryLogs(this);
                    break;

                // update user management when user has changes in their profile 
                case ChangeTypes.UpdateUserManagement:
                    PublicEvents.InvokeUpdateUserManagement(this);
                    break;

                // [update change from user management] update current password field in selected user's manage account
                case ChangeTypes.PasswordChangedByAdmin: 
                    if (change.AffectedUserId == _currentUserId)
                    {    
                        var payload = JsonSerializer.Deserialize<PasswordPayload>(change.Payload);
                        PublicEvents.InvokeUserPasswordChangedByAdmin(
                            this,
                            change.AffectedUserId.Value,
                            payload.NewHash);
                    }
                    break;

                case ChangeTypes.NewNotification: 
                    PublicEvents.InvokeUpdateNotif(this);
                    break;

                // update inventory tiles/table, n inventory notiifs 
                case ChangeTypes.InventoryChanged:
                    PublicEvents.InvokeInventoryChanged(this);
                    break;

                // update user, force signout 
                case ChangeTypes.UserDeletedByAdmin:
                    if (change.AffectedUserId == _currentUserId)
                        PublicEvents.InvokeUserDeletedByAdmin(this, _currentUserId);
                    break;

                // update user role, force signout 
                case ChangeTypes.RoleChangedByAdmin:
                    if (change.AffectedUserId == _currentUserId)
                        PublicEvents.InvokeUserRoleChangedByAdmin(this, _currentUserId);
                    break;

                // update unit, categories dropdown
                case ChangeTypes.UpdateUnit_Category:
                    PublicEvents.InvokeUpdateUnit_CategoryDropdowns(this);
                    break;
            }
        }
    }
}
