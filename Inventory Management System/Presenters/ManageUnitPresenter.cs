using Inventory_Management_System.Helpers;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    public class ManageUnitPresenter
    {
        private readonly IManageUnitView _view;
        private readonly IManageUnitRepository _repo;
        private readonly IChangeLogRepository _changeLogRepo;

        public ManageUnitPresenter(IManageUnitRepository repo, IChangeLogRepository changeLogRepo, IManageUnitView view)
        {
            _repo = repo;
            _view = view;
            _changeLogRepo = changeLogRepo;

            _view.AddOption += OnAddOption_Click;
            _view.EditOption += OnEditOption_Click;
            _view.AddNewUnit += OnAddNew_Click;
            _view.DeleteUnit += OnDelete_Click;
            _view.SaveEditChanges += OnSaveEdit_Click;
            _view.CancelChanges += OnCancel_Click;

            PublicEvents.UpdateUnit_Category += async (s, e) => await View_RefreshUnits();
        }
        public async Task View_RefreshUnits()
        {
            var units = await _repo.GetAllUnitsAsync();
            _view.PopulateUnitDetails(units);
        }

        private void OnAddOption_Click(object? sender, EventArgs e)
        {
            // show add unit side
            _view.ShowAddUnitSide();
            _view.ClearWarnings();
            _view.ClearFields();
            _view.SelectedUnit = null;
        }
        private void OnEditOption_Click(object? sender, EventArgs e)
        {
            // show edit unit side
            _view.ShowEditUnitSide();
            _view.ClearWarnings();
            _view.ClearFields();
            _view.SelectedUnit = null;
        }
        private async void OnAddNew_Click(object? sender, EventArgs e)
        {
            string newUnitName = _view.NewUnitName.Trim();
            string? validationMessage = Helpers.Validator.IsNotEmpty(newUnitName);
            bool isDuplicate = await _repo.UnitAlreadyExistsAsync(newUnitName);


            if (validationMessage != null)
            {
                _view.SetNewUnitWarning(validationMessage);
                return;
            }
            if (isDuplicate)
            {
                _view.SetNewUnitWarning("Unit already exists.");
                return;
            }

            if (validationMessage == null && !isDuplicate)
            {
                await _repo.AddUnitAsync(newUnitName);
                _view.ShowMessage("Unit added successfully.", "Success", MessageBoxIcon.Information);
                _view.ClearFields();
                _view.ClearWarnings();
                await View_RefreshUnits();
                _view.OnUnitAdded();
                await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateUnit_Category,
                           affectedUserId: null,
                           payload: null,
                           changedBy: CurrentUser.UserID
                       );
            }
        }
        private async void OnDelete_Click(object? sender, EventArgs e)
        {
            int id = _view.SelectedUnit != null ? _view.SelectedUnit.UnitId : 0;

            if (id == 0)
            {
                _view.SetChooseUnitToEditWarning("Please select a unit to delete.");
                _view.SetEditUnitWarning(string.Empty);
                return;
            }

            _view.SetEditUnitWarning(string.Empty);

            if (_view.ConfirmMessage($"Are you sure you want to delete the unit '{_view.SelectedUnit?.Unit}'?", "Confirm Delete", MessageBoxIcon.Warning))
            {
                bool isDeleted = await _repo.DeleteUnitAsync(id);
                if (isDeleted)
                {
                    _view.ShowMessage("Unit deleted successfully.", "Success", MessageBoxIcon.Information);
                    _view.ClearFields();
                    _view.ClearWarnings();
                    await View_RefreshUnits();
                    _view.RefreshInventoryTable();
                    _view.OnUnitAdded();
                    _view.SelectedUnit = null;
                    await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateUnit_Category,
                           affectedUserId: null,
                           payload: null,
                           changedBy: CurrentUser.UserID
                       );
                }
                else
                    _view.ShowMessage("Failed to delete unit. Please try again.", "Error", MessageBoxIcon.Error);
            }
        }
        private async void OnSaveEdit_Click(object? sender, EventArgs e)
        {
            int id = _view.SelectedUnit != null ? _view.SelectedUnit.UnitId : 0;
            string newUnitName = _view.EditedUnitName.Trim();
            string? validationMessageNewUnit = Helpers.Validator.IsNotEmpty(newUnitName);
            string? validationMessageSelection = Helpers.Validator.HasSelected(id);
            bool isDuplicate = await _repo.UnitAlreadyExistsAsync(newUnitName);


            if (isDuplicate)
            {
                _view.SetEditUnitWarning("Unit already exists.");
                return;
            }

            _view.SetEditUnitWarning(validationMessageNewUnit);
            _view.SetChooseUnitToEditWarning(validationMessageSelection);


            if (validationMessageNewUnit == null && !isDuplicate && validationMessageSelection == null)
            {
                await _repo.EditUnitAsync(id, newUnitName);
                _view.ShowMessage("Unit updated successfully.", "Success", MessageBoxIcon.Information);
                _view.ClearFields();
                _view.ClearWarnings();
                await View_RefreshUnits();
                _view.RefreshInventoryTable();
                _view.CloseManageUnit();
                _view.OnUnitAdded();
                _view.SelectedUnit = null;
                await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateUnit_Category,
                           affectedUserId: null,
                           payload: null,
                           changedBy: CurrentUser.UserID
                       );
            }
            else { return; }
        }
        private async void OnCancel_Click(object? sender, EventArgs e)
        {
            _view.ClearFields();
            _view.ClearWarnings();
            await View_RefreshUnits();
            _view.CloseManageUnit();
            _view.SelectedUnit = null;
        }
    }
}
