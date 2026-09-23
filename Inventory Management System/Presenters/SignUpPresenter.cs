using Inventory_Management_System.Helpers;
using Inventory_Management_System.Models;
using Inventory_Management_System.ModelsData;
using Inventory_Management_System.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Presenters
{
    public class SignUpPresenter
    {
        private readonly ISignUpView _view;
        private readonly ISignUpRepository _repo;
        private readonly IChangeLogRepository _changeLogRepo;

        public SignUpPresenter(ISignUpView view, IChangeLogRepository changeLogRepo, ISignUpRepository repo)
        {
            _view = view;
            _repo = repo;
            _changeLogRepo = changeLogRepo;

            _view.OnRegisterAccount_Click += OnRegisterAccount_Click;
            _view.OnGoToLogin_Click += OnGoToLogin_Click;

        }

        private async void OnRegisterAccount_Click(object? sender, EventArgs e)
        {
            
            var input = _view.RegisterInput;
            bool isValid = await ValidateInput(input);
            if (!isValid) return;
            else 
            {
                // Proceed with registration
                var isDbNull = await _repo.IsDBNullAsync();
                string hashedPass = PasswordHasher.HashPassword(input.Password);

                SignUpModel newUser = new SignUpModel
                {
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    UserName = input.UserName,
                    HashedPassword = hashedPass,
                    Role = isDbNull ? 1 : 2 // set first registered user as admin, others as regular users
                };
                

                try
                {
                    AppUsers user = await _repo.RegisterAsync(newUser);
                    bool isSuccessful = user != null;

                    if (user == null)
                    {
                        MessageBox.Show("An error occurred during registration. Please try again.", "Registration Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

                        await _changeLogRepo.LogChangeAsync(
                            ChangeTypes.UpdateUserManagement,
                            affectedUserId: null,
                            payload: null,
                            changedBy: CurrentUser.UserID
                        );
                        PublicEvents.InvokeUpdateUserManagement(this);
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show($"An unexpected error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OnGoToLogin_Click(object? sender, EventArgs e)
        {
            _view.ClearWarnings();
            _view.ClearFields();
            _view.NavigateToLogin();
        }

        private async Task<bool> ValidateInput(SignUpModel input)
        {
            // Implement input validation logic
            bool isValid = true;

            _view.ClearWarnings();

            string? fNameError = Validator.IsNotEmpty(input.FirstName);
            if (fNameError != null)
            {
                _view.SetFirstNameWarning(fNameError);
                isValid = false;
            }

            string? lNameError = Validator.IsNotEmpty(input.LastName);
            if (lNameError != null)
            {
                _view.SetLastNameWarning(lNameError);
                isValid = false;
            }

            string? uNameError = Validator.IsNotEmpty(input.UserName)
                ?? Validator.IsValidUsername(input.UserName);
            if (uNameError == null) {
                bool userNameExists = await _repo.UsernameTakenAsync(input);
                if (userNameExists)
                {
                    uNameError = "Username is already taken.";
                }
            }
            if (uNameError != null)
            {
                _view.SetUserNameWarning(uNameError);
                isValid = false;
            }

            string? passError = Validator.IsNotEmpty(input.Password)
                ?? Validator.IsValidPassword(input.Password);
            if (passError != null)
            {
                _view.SetPasswordWarning(passError);
                isValid = false;
            }

            string? confirmPassError = Validator.IsNotEmpty(input.ConfirmPassword)
                ?? Validator.IsSameNewPassword(input.Password, input.ConfirmPassword);
            if (confirmPassError != null)
            {
                _view.SetConfirmPasswordWarning(confirmPassError);
                isValid = false;
            }

          

            return isValid;
        }
    }
}
