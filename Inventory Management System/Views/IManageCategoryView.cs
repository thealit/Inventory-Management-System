using Inventory_Management_System.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface IManageCategoryView
    {
        public CategoryModel? SelectedCategory { get; set; }
        public string NewCategoryName { get; }
        public string EditedCategoryName { get; }

        event EventHandler AddOption;
        event EventHandler EditOption;
        event EventHandler AddNewCategory;
        event EventHandler DeleteCategory;
        event EventHandler SaveEditChanges;
        event EventHandler CancelChanges;
        event EventHandler? CategoryChanged;

        void ClearWarnings();
        void ClearFields();
        void PopulateCategoryDetails(List<CategoryModel> categories);
        void ShowMessage(string message, string header, MessageBoxIcon icon);
        bool ConfirmMessage(string message, string header, MessageBoxIcon icon);
        void SetNewCategoryWarning(string? message);
        void SetChooseCategoryToEditWarning(string? message);
        void SetEditCategoryWarning(string? message);
        void CloseManageCategory();
        void ShowAddCategorySide();
        void ShowEditCategorySide();
        void RefreshInventoryTable();
        void OnCategoryAdded();
    }
}
