using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    public class SignInPresenter
    {
        private readonly ISignInView _view;
        private readonly ISignInRepository _repo;

        public SignInPresenter(ISignInView view, ISignInRepository repo)
        {
            _view = view;
            _repo = repo;

            _view.OnLogin_Click += OnLogin_Click;
            _view.OnGoToSignUp_Click += OnGoToSignUp_Click;
        }


        private void OnGoToSignUp_Click(object? sender, EventArgs e)
        {
            _view.ClearWarnings();
            _view.ClearFields();
            _view.NavigateToRegister();
        }

        private async void OnLogin_Click(object? sender, EventArgs e)
        {
            var input = _view.LoginInput;

            bool isValid = ValidInput(_view.LoginInput);
            if (!isValid) return;
            else
            {
                AppUsers user = await _repo.LoginAsync(input);
                if (user == null)
                {
                    _view.SetPasswordWarning("Invalid username or password");
                    return;
                }
                else
                {

                    _view.ClearWarnings();
                    _view.ClearFields();

                    CurrentUser.UserID = user.UserID;
                    CurrentUser.UserName = user.UserName;
                    CurrentUser.Fullname = $"{user.FirstName} {user.LastName}";
                    await _view.NavigateToDashboard(user);

                    
                }
            }

        }

        private bool ValidInput(SignInModel input)
        {
            bool isValid = true;

            _view.ClearWarnings();

            string? uNameError = Helpers.Validator.IsNotEmpty(input.UserName);
            if (uNameError != null)
            {
                _view.SetUserNameWarning(uNameError);
                isValid = false;
            }

            string? passError = Helpers.Validator.IsNotEmpty(input.Password);
            if (passError != null)
            {
                _view.SetPasswordWarning(passError);
                isValid = false;
            }

            return isValid;
        }
    }
}
