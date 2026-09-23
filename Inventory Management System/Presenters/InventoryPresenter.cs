using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Microsoft.Data.SqlClient;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Inventory_Management_System.Presenters
{

    public class InventoryPresenter
    {
        private readonly IInventoryRepository _inventoryRepository;
        private readonly Views.IInventoryView _view;
        private readonly IChangeLogRepository _changeLogRepo;
        public event EventHandler? InventoryUpdated;

        public InventoryPresenter(InventoriesRepository inventoryRepository, IChangeLogRepository changeLogRepo, Views.IInventoryView view)
        {
            _inventoryRepository = inventoryRepository;
            _changeLogRepo = changeLogRepo;
            _view = view;

            // events
            _view.AddItemClicked += OnAddItem_Clicked;
            _view.DeleteItemClicked += OnDeleteItem_Clicked;
            _view.UpdateItemClicked += OnUpdateItem_Clicked;
            _view.CancelAddClicked += OnCancelAdd_Clicked;
            _view.SearchChanged += OnSearch_Changed;
            _view.SelectionChanged += OnSelectionChanged;
            _view.CheckBoxTopClicked += OnCheckBoxTop_Clicked;

            _view.FilterLowStockItems += OnLowStockTile_Clicked;
            _view.FilterOutOfStockItems += OnOutOfStockTile_Clicked;
            _view.FilterAllItems += OnAllStockTile_Clicked;
        }

        private async void OnInventoryChanged(object? sender, EventArgs e)
        {
            _view.SetTilesValues(
                    totalStocks: await _inventoryRepository.CountAllStocksAsync(),
                    lowStock: await _inventoryRepository.CountLowStockAsync(),
                    outOfStock: await _inventoryRepository.CountOutOfStockAsync());

            await LoadInventoryTableAsync();
            _view.InvalidateAll();
        }

        public async Task Initialize()
        {
            // Populate Combo boxes
            _view.PopulateCategories(await _inventoryRepository.GetCategoriesAsync());
            _view.PopulateUnits(await _inventoryRepository.GetUnitsAsync());

            _view.ClearItemFields();
            _view.SetUpActionButtons();
            _view.DataGridViewSetUp();
            _view.ToggleActionMode(state: 0); // 0 hide all
            _view.SetCheckBoxTopStatusIcon(0);
            _view.SetFieldsHeadersBold();
            _view.SetTilesValues(
                totalStocks: await _inventoryRepository.CountAllStocksAsync(),
                lowStock: await _inventoryRepository.CountLowStockAsync(),
                outOfStock: await _inventoryRepository.CountOutOfStockAsync());

            // Load table
            //await LoadInventoryTableAsync();
            await _inventoryRepository.GetAllInventoriesAsync(); // fixed
            PublicEvents.InventoryChanged += OnInventoryChanged;
            PublicEvents.UpdateUnit_Category += OnUpdateUnit_Category;
        }

        private async void OnUpdateUnit_Category(object? sender, EventArgs e)
        {
            // trigger refresh across users when categ n unit changed
            await RefreshCategories();
            await RefreshUnits();
        }

        public async Task RefreshCategories()
        {
            _view.PopulateCategories(await _inventoryRepository.GetCategoriesAsync());
        }

        public async Task RefreshUnits()
        {
            _view.PopulateUnits(await _inventoryRepository.GetUnitsAsync());
        }

        public async Task LoadInventoryTableAsync()
        { 
            var items = await _inventoryRepository.GetAllInventoriesAsync();

            if (items.Count == 0) _view.ShowMessage("No items listed in inventory.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

            _view.RefreshInventoryTable(items);
        }

        public async void OnSearch_Changed(object? sender, EventArgs e)
        {
            string term = _view.SearchItem;

            if (string.IsNullOrEmpty(term))
            {
                await LoadInventoryTableAsync();
                return;
            }

            var results = await _inventoryRepository.SearchInventoryAsync(term);

            if (!string.IsNullOrEmpty(term) && results.Count == 0) {
                _view.ShowMessage($"No results found for \"{term.Trim()}\".", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                _view.RefreshInventoryTable(results);
            }
        }
        
        public async void OnAddItem_Clicked(object? sender, EventArgs e)
        {
            //validations here
            bool isInputCorrect = ValidateInput(_view.Item, _view.CategoryId, _view.InStock, _view.UnitId, _view.MinStock);
            if (!isInputCorrect) return;

            bool isItemADuplicate = await _inventoryRepository.DoesItemExistsAsync(_view.Item, 0);


            try
            {
                if (isItemADuplicate)
                {
                    _view.ShowMessage($"An item with the name \"{_view.Item}\" already exists in the inventory. Please choose a different name.", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var newItem = new InventoryModel
                {
                    Item = _view.Item,
                    InStock = int.Parse(_view.InStock),
                    MinimumStock = int.Parse(_view.MinStock),
                    CategoryId = _view.CategoryId,
                    UnitId = _view.UnitId
                };

                bool isSuccess = await _inventoryRepository.AddAsync(newItem);
                string action = $"Added the item \"{_view.Item}\".";
                if (isSuccess)
                {
                    _view.ShowMessage("Item successfully added to inventory!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.ClearItemFields();
                    _view.ToggleActionMode(2);

                    await _changeLogRepo.LogChangeAsync(
                         ChangeTypes.InventoryChanged,
                         affectedUserId: null,    // affect all
                         payload: null,
                         changedBy: CurrentUser.UserID
                       );
                    PublicEvents.InvokeInventoryChanged(this);
                   
                    await _inventoryRepository.LogInventoryActivity(CurrentUser.Fullname ?? $"{(CurrentUser.IsAdmin ? "Admin" : "Staff")}", action);

                    await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateAuditTrail,
                           affectedUserId: null,    // affect all
                           payload: null,
                           changedBy: CurrentUser.UserID
                       );

                    PublicEvents.InvokeUpdateInventoryLogs(this);
                }
                else {
                    _view.ShowMessage("Failed to add new item.", "Fail", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                _view.ShowMessage($"Failed to save to the database.\nError: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public async void OnDeleteItem_Clicked(object? sender, EventArgs e) 
        {
            if (_view.ItemIds == null || _view.ItemIds.Count == 0)
            {
                _view.ShowMessage("Please select at least one item to delete.", "No Selection", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            bool confirmedDeletion = _view.ConfirmAction("The selected item(s) will be permanently deleted. Do you want to proceed?", "Confirm Delete");

            string action = string.Empty;
            if (_view.ItemIds.Count > 1)
                action = $"Deleted the items \"{string.Join(", ", _view.ItemIds.Select(item => item.Value))}\".";
            else if (_view.ItemIds.Count == 1)
                action = $"Deleted the item \"{_view.ItemIds.First().Value}\".";

            if (confirmedDeletion)
            {
                try
                {
                    string term = _view.ItemIds.Count > 1 ? "Items" : "Item";


                    await _inventoryRepository.DeleteAsync(_view.ItemIds.Select(items => items.Key).ToList());

                    _view.ShowMessage($"{term} successfully removed from inventory!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    _view.ClearItemFields();

                    await _changeLogRepo.LogChangeAsync(
                         ChangeTypes.InventoryChanged,
                         affectedUserId: null,    // affect all
                         payload: null,
                         changedBy: CurrentUser.UserID
                       );
                    PublicEvents.InvokeInventoryChanged(this); 
         
                    await _inventoryRepository.LogInventoryActivity(CurrentUser.Fullname ?? $"{(CurrentUser.IsAdmin ? "Admin" : "Staff")}", action);

                    await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateAuditTrail,
                           affectedUserId: null,    // affect all
                           payload: null,
                           changedBy: CurrentUser.UserID
                       );

                    PublicEvents.InvokeUpdateInventoryLogs(this);
                }
                catch (Exception ex)
                {
                    _view.ShowMessage($"Failed to apply changes.\nError: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else { return; }

        }

        public async void OnUpdateItem_Clicked(object? sender, EventArgs e) 
        {
            var selectedItem = _view.SelectedItem;

            if (selectedItem == null)
            {
                _view.ShowMessage("Please select an item from the table to edit.", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

           
            bool isInputCorrect = ValidateInput(_view.Item, _view.CategoryId, _view.InStock, _view.UnitId, _view.MinStock);
            if (!isInputCorrect) return;

            bool hasChanges = false;
            List<string> changes = new();

            if (selectedItem != null)
            {
                if (selectedItem.Item != _view.Item)
                {
                    changes.Add($"renamed item from \"{selectedItem.Item}\" to \"{_view.Item}\"");
                    hasChanges = true;
                }
                if (selectedItem.InStock != int.Parse(_view.InStock))
                {
                    changes.Add($"changed stock from {selectedItem.InStock} to {_view.InStock}");
                    hasChanges = true;
                }
                if (selectedItem.MinimumStock != int.Parse(_view.MinStock))
                {
                    changes.Add($"changed minimum stock from {selectedItem.MinimumStock} to {_view.MinStock}");
                    hasChanges = true;
                }
                if (selectedItem.CategoryId != _view.CategoryId)
                {
                    changes.Add($"changed category from \"{selectedItem.Category}\" to \"{await GetCategoryName(_view.CategoryId)}\"");
                    hasChanges = true;
                }

                if (selectedItem.UnitId != _view.UnitId)
                {
                    changes.Add($"changed unit from \"{selectedItem.ItemUnit}\" to \"{await GetUnitName(_view.UnitId)}\"");
                    hasChanges = true;
                }
            }

            if (hasChanges == false)
            {
                _view.ShowMessage("No new edits were found.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            bool isItemADuplicate = await _inventoryRepository.DoesItemExistsAsync(_view.Item, selectedItem.ItemId);
           

            if (isItemADuplicate)
            {
                _view.ShowMessage($"An item with the name \"{_view.Item}\" already exists in the inventory. Please choose a different name.", "Duplicate Item", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                _view.FocusOnItemField();
                return;
            }

           

            string action = changes.Count > 0
                ? $"Updated \"{selectedItem.Item}\": {string.Join(", ", changes)}."
                : $"Updated \"{selectedItem.Item}\" with no changes.";

            // if ok, add to db : else, try again
            if (isInputCorrect && !isItemADuplicate && hasChanges)
            {
                try
                {
                    var item = new InventoryModel {
                        ItemId = selectedItem.ItemId,
                        Item = _view.Item,
                        CategoryId = _view.CategoryId,
                        InStock = int.Parse(_view.InStock),
                        MinimumStock = int.Parse(_view.MinStock),
                        UnitId = _view.UnitId
                    };
                    
                    bool isSuccess = await _inventoryRepository.UpdateAsync(item);

                    if (isSuccess)
                    {
                        _view.ShowMessage("Item successfully updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        _view.ClearItemFields();
                        _view.ClearWarnings();

                        // log change for poll
                        await _changeLogRepo.LogChangeAsync(
                          ChangeTypes.InventoryChanged,
                          affectedUserId: null,    // affect all
                          payload: null,
                          changedBy: CurrentUser.UserID
                        );

                        // reload local
                        PublicEvents.InvokeInventoryChanged(this);

                        await _inventoryRepository.LogInventoryActivity(CurrentUser.Fullname ?? $"{(CurrentUser.IsAdmin ? "Admin" : "Staff")}", action);

                        await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateAuditTrail,
                           affectedUserId: null,    // affect all
                           payload: null,
                           changedBy: CurrentUser.UserID
                        );

                        PublicEvents.InvokeUpdateInventoryLogs(this);
                    }
                    else {
                        _view.ShowMessage("Failed to update item.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    _view.ShowMessage($"Failed to save changes to the database.\nError: {ex.Message}", "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            else { return; }

        }

        private async Task<string> GetCategoryName(int categoryId)
        {
            var categories = await _inventoryRepository.GetCategoriesAsync();
            return categories.FirstOrDefault(c => c.CategoryId == categoryId)?.Category ?? "Unknown";
        }

        private async Task<string> GetUnitName(int unitId)
        {
            var units = await _inventoryRepository.GetUnitsAsync();
            return units.FirstOrDefault(u => u.UnitId == unitId)?.Unit ?? "Unknown";
        }

        public void OnCancelAdd_Clicked(object? sender, EventArgs e) 
        {
            bool confirmedDiscard = false;


            bool hasAnyInput = _view.AnyTxtBoxesIsNotEmpty() || _view.AnyComboBoxesHasSelection();

            if (hasAnyInput)
            {
                confirmedDiscard = _view.ConfirmAction("Item will not be saved. Do you want to proceed?", "Confirm Discard");
            }

            if ((hasAnyInput && confirmedDiscard) || !hasAnyInput)
            {
                _view.ClearItemFields();
                _view.ClearWarnings();
                _view.ToggleActionMode(2);

                // repopulate item details
                var selectedItem = _view.SelectedItem;
                if (selectedItem != null)
                {
                    _view.PopulateItemDetails(selectedItem); 
                }
                else
                {
                    _view.ToggleActionMode(0);
                }
            }
            else { return; }

        }

        private int _tableFilterState = 0; // 0-all, 1-low, 2-out
        public async void OnLowStockTile_Clicked(object? sender, EventArgs e) 
        {
            if (_tableFilterState == 1) return;

            _tableFilterState = 1;
            try
            {
                var lowStock = await _inventoryRepository.GetLowStockAsync();

                if (lowStock.Count == 0)
                {
                    _view.ShowMessage("No low stock items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadInventoryTableAsync();
                }
                else
                {
                    _view.RefreshInventoryTable(lowStock);
                }
            }
            catch (Exception)
            {
                _view.ShowMessage("Failed to load low stock items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadInventoryTableAsync();
            }
        }
        public async void OnOutOfStockTile_Clicked(object? sender, EventArgs e) 
        {
            if (_tableFilterState == 2) return;

            _tableFilterState = 2;

            try
            {
                var outOfStock = await _inventoryRepository.GetOutOfStockAsync();

                if (outOfStock.Count == 0)
                {
                    _view.ShowMessage("No out of stock items.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    await LoadInventoryTableAsync();
                }
                else
                {
                    _view.RefreshInventoryTable(outOfStock);
                }

            }
            catch (Exception)
            {
                _view.ShowMessage("Failed to load out of stock items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                await LoadInventoryTableAsync();
            }
        }

        public async void OnAllStockTile_Clicked(object? sender, EventArgs e) 
        {
            if (_tableFilterState == 0 && string.IsNullOrWhiteSpace(_view.SearchItem)) return;

            _tableFilterState = 0;

            try
            {
                await LoadInventoryTableAsync();
            }
            catch (Exception)
            {
                _view.ShowMessage("Failed to all items.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }




        //default/onfirst load: unchecked
        //toggle 1: check all
        //if few selections on grid: indeterminate
        //if in indeterminate status clicked: unchecked
        private int toggle = 0; // 0: unchecked, 1: checkedAll, 2: indeterminate

        public void OnCheckBoxTop_Clicked(object? sender, EventArgs e)
        {
            int count = _view.CheckedCount;

            if (toggle == 0)
            {
                toggle = 1;
                _view.SetCheckBoxTopStatusIcon(status: 1);
                _view.SetAllRowsCheckboxesState(checkAll: true);
            }
            else if (toggle ==  1 || toggle == 2)
            {
                toggle = 0;
                _view.SetCheckBoxTopStatusIcon(status: 0);
                _view.SetAllRowsCheckboxesState(checkAll: false);
            }

            UpdateSelectionState();
        }

        private void OnSelectionChanged(object? sender, EventArgs e)
        {
            UpdateSelectionState();
            _view.ClearWarnings();
            _view.InvokeOnUI(() => UpdateSelectionState());
        }

        public void UpdateSelectionState()
        {
            int count = _view.CheckedCount;
            var selected = _view.SelectedItem;
            bool rowIsSelected = false;
            _view.SetTopDeleteButton(count);

            if (count == 0 && selected != null)
            {
                rowIsSelected = true;
                _view.PopulateItemDetails(selected);
                _view.ToggleActionMode(state: 2); // show edit/delete buttons
                _view.SetCheckBoxTopStatusIcon(status: 0);
                return; 
            }

            if (count == 0 && rowIsSelected == false) // no checked
            {
                _view.SetCheckBoxTopStatusIcon(status: 0);
                _view.ToggleActionMode(state: 0);
                _view.ClearItemFields();
            }
            else if (count == 1 && selected != null) // selected row
            {
                _view.SetCheckBoxTopStatusIcon(status: 2);
                _view.PopulateItemDetails(selected);
                _view.ToggleActionMode(state: 2);
            }
            else if (count > 0 && !_view.AreAllRowsChecked) // in indeterminate state
            {
                _view.SetCheckBoxTopStatusIcon(status: 2);
                _view.ClearItemFields();
                _view.ToggleActionMode(state: 0); // hide all buttons
            }
            else if (_view.AreAllRowsChecked) // is checktop checked: check all
            {
                _view.SetCheckBoxTopStatusIcon(status: 1);
                _view.ClearItemFields();
                _view.ToggleActionMode(state: 0);
            }
        }

        private bool ValidateInput(string item, int categoryId, string inStock, int unitId, string minStock)
        {
            bool isValid = true;

            _view.ClearWarnings();

            // Item 
            string? itemError = Helpers.Validator.IsNotEmpty(item);
            _view.SetItemWarning(itemError);
            if (itemError != null) isValid = false;

            // Category
            string? categoryError = Helpers.Validator.HasSelected(categoryId);
            _view.SetCategoryWarning(categoryError);
            if (categoryError != null) isValid = false;

            // InStock 
            string? inStockError = Helpers.Validator.IsNotEmpty(inStock);
            if (inStockError == null) inStockError = Helpers.Validator.IsValidNum(inStock);
            _view.SetInStockWarning(inStockError);
            if (inStockError != null) isValid = false;

            // Unit
            string? unitError = Helpers.Validator.HasSelected(unitId);
            _view.SetUnitWarning(unitError);
            if (unitError != null) isValid = false;

            // MinStock 
            string? minStockError = Helpers.Validator.IsNotEmpty(minStock);
            if (minStockError == null) minStockError = Helpers.Validator.IsValidNum(minStock);
            _view.SetMinStockWarning(minStockError);
            if (minStockError != null) isValid = false;

            return isValid;
        }
    }
}
