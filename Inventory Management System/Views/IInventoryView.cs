using Inventory_Management_System.Components;
using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    // facing ui actions

    public interface IInventoryView
    {
        string SearchItem { get; set; }

        // input handler
        int ItemId { get; }
        string Item { get; }
        string InStock { get; }
        string MinStock { get; }
        int CategoryId { get; set; } 
        int UnitId { get; set; }
        List<KeyValuePair<int, string>> ItemIds { get; }
        int ItemToUpdate { get; }
        InventoryModel? SelectedItem { get; }
        int CheckedCount { get; }
        bool AreAllRowsChecked { get; }

        event EventHandler AddItemClicked;
        event EventHandler DeleteItemClicked;
        event EventHandler UpdateItemClicked;
        event EventHandler CancelAddClicked;
        event EventHandler SearchChanged;
        event EventHandler SelectionChanged;
        event EventHandler CheckBoxTopClicked;
        event EventHandler FilterLowStockItems;
        event EventHandler FilterOutOfStockItems;
        event EventHandler FilterAllItems;

        void ShowMessage(string message, string errorHeader, MessageBoxButtons btn, MessageBoxIcon icon); 
        bool ConfirmAction(string message, string title); 
        bool AnyTxtBoxesIsNotEmpty(); 
        bool AnyComboBoxesHasSelection(); 
        void ClearItemFields();
        void ClearWarnings(); 
        void RefreshInventoryTable(List<InventoryModel> items);  
        void FocusOnItemField();
        void SetItemWarning(string? message); 
        void SetCategoryWarning(string? message); 
        void SetInStockWarning(string? message); 
        void SetUnitWarning(string? message); 
        void SetMinStockWarning(string? message); 
        void PopulateCategories(List<CategoryModel> categories);
        void PopulateUnits(List<UnitModel> units);
        void PopulateItemDetails(InventoryModel item);
        void ToggleActionMode(int state);
        void SetTopDeleteButton(int count);
        void SetDefaultFields();
        void SetCheckBoxTopStatusIcon(int status); // 0: unchecked, 1: checkedAll, 2: indeterminate
        void SetAllRowsCheckboxesState(bool checkAll);
        void SetTilesValues(int totalStocks, int lowStock, int outOfStock);
        void SetUpActionButtons();
        void DataGridViewSetUp();
        void SetFieldsHeadersBold();
        void InvokeOnUI(Action action);
        void InvalidateAll();
    }
}