using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Views
{
    public interface ISignInView
    {
        public SignInModel LoginInput { get; }

        event EventHandler? OnLogin_Click;
        event EventHandler? OnGoToSignUp_Click;

        void ClearWarnings();
        void ClearFields();
        void ShowMessage(string message, string header, MessageBoxIcon icon);
        void SetUserNameWarning(string? message);
        void SetPasswordWarning(string? message);
        void NavigateToRegister();
        Task NavigateToDashboard(AppUsers user); 

    }
}
