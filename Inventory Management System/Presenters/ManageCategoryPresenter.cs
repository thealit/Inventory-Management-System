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
    public class ManageCategoryPresenter
    {
        private readonly IManageCategoryView _view;
        private readonly IManageCategoryRepository _repo;
        private readonly IChangeLogRepository _changeLogRepo;

        public ManageCategoryPresenter(IManageCategoryRepository repo, IChangeLogRepository changeLogRepo, IManageCategoryView view)
        {
            _repo = repo;
            _view = view;
            _changeLogRepo = changeLogRepo;

            _view.AddOption += OnAddOption_Click;
            _view.EditOption += OnEditOption_Click; 
            _view.AddNewCategory += OnAddNew_Click;
            _view.DeleteCategory += OnDelete_Click;
            _view.SaveEditChanges += OnSaveEdit_Click;
            _view.CancelChanges += OnCancel_Click;

            PublicEvents.UpdateUnit_Category += async (s,e) => await View_RefreshCategories();
        }
        public async Task View_RefreshCategories()
        {
            var categories = await _repo.GetAllCategoriesAsync();
            _view.PopulateCategoryDetails(categories);
        }

        private void OnAddOption_Click(object? sender, EventArgs e)
        {
            // show add category side
            _view.ShowAddCategorySide();
            _view.ClearWarnings();
            _view.ClearFields();
        }
        private void OnEditOption_Click(object? sender, EventArgs e)
        {
            // show edit category side
            _view.ShowEditCategorySide();
            _view.ClearWarnings();
            _view.ClearFields();
            _view.SelectedCategory = null;
        }
        private async void OnAddNew_Click(object? sender, EventArgs e)
        {
            string newCategoryName = _view.NewCategoryName.Trim();
            string? validationMessage = Helpers.Validator.IsNotEmpty(newCategoryName);
            bool isDuplicate = await _repo.CategoryAlreadyExistsAsync(newCategoryName);


            if (validationMessage != null)
            {
                _view.SetNewCategoryWarning(validationMessage);
                return;
            }
            if (isDuplicate)
            {
                _view.SetNewCategoryWarning("Category already exists.");
                return;
            }

            if (validationMessage == null && !isDuplicate)
            {
                await _repo.AddCategoryAsync(newCategoryName);
                _view.ShowMessage("Category added successfully.", "Success", MessageBoxIcon.Information);
                _view.ClearFields();
                _view.ClearWarnings();
                await View_RefreshCategories();
                _view.OnCategoryAdded();

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
            int id = _view.SelectedCategory != null ? _view.SelectedCategory.CategoryId : 0;
            if (id == 0)
            {
                _view.SetChooseCategoryToEditWarning("Please select a category to delete.");
                return;
            }
            _view.SetEditCategoryWarning(string.Empty);

            if (_view.ConfirmMessage($"Are you sure you want to delete the category '{_view.SelectedCategory?.Category}'?", "Confirm Delete", MessageBoxIcon.Warning))
            {
                bool isDeleted = await _repo.DeleteCategoryAsync(id);
                if (isDeleted)
                {
                    _view.ShowMessage("Category deleted successfully.", "Success", MessageBoxIcon.Information);
                    _view.ClearFields();
                    _view.ClearWarnings();
                    await View_RefreshCategories();
                    _view.RefreshInventoryTable();
                    _view.OnCategoryAdded();
                    _view.SelectedCategory = null;
                    await _changeLogRepo.LogChangeAsync(
                           ChangeTypes.UpdateUnit_Category,
                           affectedUserId: null,
                           payload: null,
                           changedBy: CurrentUser.UserID
                       );
                }
                else
                    _view.ShowMessage("Failed to delete category. Please try again.", "Error", MessageBoxIcon.Error);
            }
        }
        private async void OnSaveEdit_Click(object? sender, EventArgs e)
        {

            int id = _view.SelectedCategory != null ? _view.SelectedCategory.CategoryId : 0;
            string newCategoryName = _view.EditedCategoryName.Trim();
            string? validationMessageNewCategory = Helpers.Validator.IsNotEmpty(newCategoryName);
            string? validationMessageSelection = Helpers.Validator.HasSelected(id);
            bool isDuplicate = await _repo.CategoryAlreadyExistsAsync(newCategoryName);

            _view.SetChooseCategoryToEditWarning(validationMessageSelection);
            _view.SetEditCategoryWarning(validationMessageNewCategory);

            if (isDuplicate)
            {
                _view.SetEditCategoryWarning("Category already exists.");
                return;
            }

            if (validationMessageNewCategory == null && !isDuplicate && validationMessageSelection == null)
            {
                await _repo.EditCategoryAsync(id, newCategoryName);
                _view.ShowMessage("Category updated successfully.", "Success", MessageBoxIcon.Information);
                _view.ClearFields();
                _view.ClearWarnings();
                await View_RefreshCategories();
                _view.RefreshInventoryTable();
                _view.CloseManageCategory();
                _view.OnCategoryAdded();
                _view.SelectedCategory = null;
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
            await View_RefreshCategories();
            _view.CloseManageCategory();
            _view.SelectedCategory = null;
        }
    }
}
