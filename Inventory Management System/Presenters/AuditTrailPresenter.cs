using Inventory_Management_System.Helpers;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Views;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    public class AuditTrailPresenter
    {
        private readonly IAuditTrailRepository _repo;
        private readonly IAuditTrailView _view;
        private readonly EventHandler _updateLogsHandler;

        public AuditTrailPresenter(IAuditTrailRepository repo, IAuditTrailView view)
        {
            _repo = repo;
            _view = view;

            _view.SearchChanged += OnSearchChanged;

            _updateLogsHandler = async (s, e) => await LoadInventoryLogs();
            PublicEvents.UpdateInventoryLogs += _updateLogsHandler;
        }



        private async void OnSearchChanged(object? sender, EventArgs e)
        {
            string term = _view.SearchLog;

            if (string.IsNullOrEmpty(term))
            {
                await LoadInventoryLogs();
                return;
            }

            var results = await _repo.SearchInventoryLogsAsync(term);

            if (!string.IsNullOrEmpty(term) && results.Count == 0)
            {
                _view.LoadLogsTable(results);
                _view.ShowMessage($"No results found for \"{term.Trim()}\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _view.LoadLogsTable(results);
            }
        }

        public async Task LoadInventoryLogs()
        {
            try
            {
                var logs = await _repo.GetAllInventoryLogsAsync();
                _view.LoadLogsTable(logs);
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"An error occurred while loading logs: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }
    }
}
