using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface ISignUpView
    {
        public SignUpModel RegisterInput { get; }

        event EventHandler? OnRegisterAccount_Click;
        event EventHandler? OnGoToLogin_Click;

        void ClearWarnings();
        void ClearFields();
        void ShowMessage(string message, string header, MessageBoxIcon icon);
        void SetFirstNameWarning(string? message);
        void SetLastNameWarning(string? message);
        void SetUserNameWarning(string? message);
        void SetPasswordWarning(string? message);
        void SetConfirmPasswordWarning(string? message);
        void NavigateToLogin(); 
        Task NavigateToDashboard(AppUsers user);
    }
}
