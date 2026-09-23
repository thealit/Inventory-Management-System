using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory_Management_System.Helpers
{
    public static class Validator
    {

        public static string? IsNotEmpty(string input) 
        {
            // Input must not be null, empty, or whitespace
            bool isEmpty = false;

            string error_msg = "This field is required.";
            if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input)) isEmpty = true;

            return isEmpty ? error_msg : null;
        }

        public static string? HasSelected(int? input)
        {
            // For combobox, check if option is selected
            string error_msg = "Please select an option.";
            return (input == null || input < 1) ? error_msg : null;
        }

        public static string? IsValidNum(string input)
        {   
            if (!int.TryParse(input, out int value) || input.Any(c => !char.IsDigit(c)))
                return "Please input a number.";

            if (value < 0)
                return "Number cannot be negative.";

            return null;
        }

        public static string? IsValidUsername(string username)
        {
            // Username must be 4-30 characters long and contain only letters and digits
            bool withinLength = username.Length >= 4 && username.Length <= 30;
            bool validChars = username.All(char.IsLetterOrDigit);

            if (withinLength && validChars)
                return null;

            List<string> errors = new List<string>();

            if (!withinLength)
                errors.Add("be 4-30 characters long");
            if (!validChars)
                errors.Add("contain only letters and digits");

            return errors.Count > 1 
                ?  ("Username must " + string.Join(" and ", errors) + ".") 
                : null;
        }


        public static string? IsValidPassword(string password)
        {
            // Password must contain at least one uppercase letter, one lowercase letter, one digit, and one special character

            bool hasUpper = false, hasLower = false, hasDigit = false, hasSpecial = false;

            foreach (char c in password)
            {
                if (char.IsUpper(c)) hasUpper = true;
                if (char.IsLower(c)) hasLower = true;
                if (char.IsDigit(c)) hasDigit = true;
                if (char.IsSymbol(c) || char.IsPunctuation(c)) hasSpecial = true;
            }

            List<string> errors = new List<string>();
            if (hasUpper == false)
                errors.Add("an uppercase letter");
            if (hasLower == false)
                errors.Add("a lowercase letter");
            if (hasDigit == false)
                errors.Add("a digit");
            if (hasSpecial == false)
                errors.Add("a special character");

            if (errors.Count == 0) return null;

            string allErrors = errors.Count == 1
                ? errors[0]
                : string.Join(", ", errors.Take(errors.Count - 1)) + ", and " + errors.Last();

            return "Password must contain " + allErrors + "."; 
        }
        

        public static string? IsSameNewPassword(string newPassword, string confirmPassword) 
        { 
            string error_msg = "Passwords do not match."; 
            return (newPassword == confirmPassword) ? null : error_msg;
        }

        public static string? IsSameCurrentPassword(string rawPass, string storedHashedPass)
        {
            string error_msg = "Password is incorrect.";
            bool isVerified = PasswordHasher.VerifyPassword(rawPass, storedHashedPass);

            return (isVerified) ? null : error_msg;
        }
    }
}
