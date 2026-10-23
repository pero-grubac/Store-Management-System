using System.Text.RegularExpressions;

namespace Prodavnica.Services
{
    public enum UserField
    {
        FirstName,
        LastName,
        UserName,
        Email,
        Phone,
        Password,
        ConfirmPassword
    }

    public enum UserError
    {
        Required,
        TooLong,
        InvalidUserName,
        InvalidEmail,
        PasswordTooShort,
        PasswordsDoNotMatch,
        DuplicateUserName
    }

    public sealed record UserValidationError(UserField Field, UserError Error);

    public sealed class UserInput
    {
        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string UserName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public string Phone { get; init; } = string.Empty;
        public string Password { get; init; } = string.Empty;
        public string ConfirmPassword { get; init; } = string.Empty;
    }

    public static partial class UserValidator
    {
        public const int MinPasswordLength = 6;
        public const int MaxLength = 45;
        public const int MaxEmailLength = 100;

        // passwordRequired: true for a new account; when editing, an empty password keeps the old one.
        public static List<UserValidationError> Validate(UserInput input, bool passwordRequired, bool userNameTaken)
        {
            var errors = new List<UserValidationError>();
            Text(errors, UserField.FirstName, input.FirstName, MaxLength);
            Text(errors, UserField.LastName, input.LastName, MaxLength);
            Text(errors, UserField.Phone, input.Phone, MaxLength);

            string userName = input.UserName.Trim();
            if (userName.Length == 0) errors.Add(new(UserField.UserName, UserError.Required));
            else if (userName.Length < 3 || userName.Length > MaxLength || !UserNamePattern().IsMatch(userName)) errors.Add(new(UserField.UserName, UserError.InvalidUserName));
            else if (userNameTaken) errors.Add(new(UserField.UserName, UserError.DuplicateUserName));

            string email = input.Email.Trim();
            if (email.Length == 0) errors.Add(new(UserField.Email, UserError.Required));
            else if (!IsValidEmail(email)) errors.Add(new(UserField.Email, UserError.InvalidEmail));

            if (passwordRequired || input.Password.Length > 0 || input.ConfirmPassword.Length > 0)
            {
                if (input.Password.Length == 0) errors.Add(new(UserField.Password, UserError.Required));
                else if (input.Password.Length < MinPasswordLength) errors.Add(new(UserField.Password, UserError.PasswordTooShort));
                if (input.ConfirmPassword != input.Password) errors.Add(new(UserField.ConfirmPassword, UserError.PasswordsDoNotMatch));
            }
            return errors;
        }

        public static bool IsValidEmail(string? email)
        {
            return !string.IsNullOrWhiteSpace(email) && email.Trim().Length <= MaxEmailLength && EmailPattern().IsMatch(email.Trim());
        }

        private static void Text(List<UserValidationError> errors, UserField field, string value, int max)
        {
            if (string.IsNullOrWhiteSpace(value)) errors.Add(new(field, UserError.Required));
            else if (value.Trim().Length > max) errors.Add(new(field, UserError.TooLong));
        }

        [GeneratedRegex(@"^[A-Za-z0-9._-]+$")]
        private static partial Regex UserNamePattern();

        [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$")]
        private static partial Regex EmailPattern();
    }
}
