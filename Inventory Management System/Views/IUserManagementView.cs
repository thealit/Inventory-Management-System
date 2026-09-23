using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface IUserManagementView
    {
        string SearchUser { get; set; }
        UpdateUserRoleModel GetUpdateUserRoleModel { get; }
        AppUsers SelectedUser { get; }

        int CheckedCount { get; }
        bool AreAllRowsChecked { get; }
        List<DeleteUsersModel> DeleteUsers { get; }

        event EventHandler EditUserClicked;
        event EventHandler SearchChanged;
        event EventHandler DeleteUserClicked;
        event EventHandler OnResetUserRoleClicked;
        event EventHandler SelectionChanged;
        event EventHandler CheckBoxTopClicked; 

        void ShowMessage(string message, string errorHeader, MessageBoxButtons btn, MessageBoxIcon icon);
        bool ConfirmAction(string message, string title);
        void RefreshUsersTable(List<AppUsers> users);
        void DataGridViewSetUp();
        void PassDataToEditUserControl(AppUsers user);

        void SetTopDeleteButton(int count);
        void SetCheckBoxTopStatusIcon(int status); // 0: unchecked, 1: checkedAll, 2: indeterminate
        void SetAllRowsCheckboxesState(bool checkAll);
    }
}
