using Prodavnica.Database;
using Prodavnica.Database.DTO;
using Prodavnica.Services;

namespace Prodavnica.Tests.Unit
{
    public class UserValidatorTests
    {
        private static UserInput Valid(string? userName = null, string? email = null, string password = "tajna123", string? confirm = null) => new UserInput
        {
            FirstName = "Ana",
            LastName = "Jovanović",
            UserName = userName ?? "ajovanovic",
            Email = email ?? "ana@prodavnica.ba",
            Phone = "065/111-222",
            Password = password,
            ConfirmPassword = confirm ?? password
        };

        [Fact]
        public void A_complete_new_user_is_valid()
        {
            Assert.Empty(UserValidator.Validate(Valid(), passwordRequired: true, userNameTaken: false));
        }

        [Fact]
        public void A_new_user_needs_a_password()
        {
            var errors = UserValidator.Validate(Valid(password: ""), passwordRequired: true, userNameTaken: false);

            Assert.Contains(new UserValidationError(UserField.Password, UserError.Required), errors);
        }

        [Fact]
        public void When_editing_an_empty_password_keeps_the_old_one()
        {
            Assert.Empty(UserValidator.Validate(Valid(password: ""), passwordRequired: false, userNameTaken: false));
        }

        [Fact]
        public void Short_or_mismatched_passwords_are_rejected()
        {
            Assert.Contains(new UserValidationError(UserField.Password, UserError.PasswordTooShort),
                UserValidator.Validate(Valid(password: "abc"), true, false));
            Assert.Contains(new UserValidationError(UserField.ConfirmPassword, UserError.PasswordsDoNotMatch),
                UserValidator.Validate(Valid(password: "tajna123", confirm: "tajna124"), true, false));
        }

        [Theory]
        [InlineData("ab")]
        [InlineData("ana jovanovic")]
        [InlineData("ana@shop")]
        public void User_names_are_three_or_more_safe_characters(string userName)
        {
            Assert.Contains(new UserValidationError(UserField.UserName, UserError.InvalidUserName),
                UserValidator.Validate(Valid(userName: userName), true, false));
        }

        [Fact]
        public void A_taken_user_name_is_reported()
        {
            Assert.Contains(new UserValidationError(UserField.UserName, UserError.DuplicateUserName),
                UserValidator.Validate(Valid(), true, userNameTaken: true));
        }

        [Theory]
        [InlineData("ana@prodavnica.ba", true)]
        [InlineData("a.b-c@mail.example.com", true)]
        [InlineData("ana", false)]
        [InlineData("ana@", false)]
        [InlineData("ana @x.ba", false)]
        [InlineData("", false)]
        public void Email_addresses_are_checked(string email, bool valid)
        {
            Assert.Equal(valid, UserValidator.IsValidEmail(email));
        }
    }

    public class PagingTests
    {
        [Theory]
        [InlineData(0, 20, 0)]
        [InlineData(1, 20, 0)]
        [InlineData(3, 20, 40)]
        [InlineData(2, 9999, 500)]
        public void Offset_uses_a_safe_page_and_page_size(int page, int size, int expectedOffset)
        {
            Assert.Equal(expectedOffset, new PageRequest(page, size).Offset);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("  ", null)]
        [InlineData(" mli ", "%mli%")]
        [InlineData("50%", "%50\\%%")]
        [InlineData("a_b", "%a\\_b%")]
        public void Search_text_becomes_an_escaped_contains_pattern(string? search, string? expected)
        {
            Assert.Equal(expected, new PageRequest(Search: search).Like);
        }

        [Fact]
        public void Paged_result_describes_its_position()
        {
            var result = new PagedResult<int>(new[] { 1, 2, 3 }, TotalCount: 43, Page: 3, PageSize: 20);

            Assert.Equal(3, result.PageCount);
            Assert.True(result.HasPrevious);
            Assert.False(result.HasNext);
            Assert.Equal(41, result.FirstIndex);
            Assert.Equal(43, result.LastIndex);
        }

        [Fact]
        public void An_empty_result_still_has_one_page()
        {
            var result = new PagedResult<int>(Array.Empty<int>(), 0, 1, 20);

            Assert.Equal(1, result.PageCount);
            Assert.Equal(0, result.FirstIndex);
        }
    }

    public class TotalsTests
    {
        [Fact]
        public void Of_an_empty_list_is_zero()
        {
            Assert.Equal(0m, Totals.Of(new List<BillItem>()));
        }

        [Fact]
        public void Of_keeps_decimal_precision()
        {
            var lines = new[] { TestData.BillItem(1, 3m, 0.1m), TestData.BillItem(2, 1.5m, 9.88m) };

            Assert.Equal(15.12m, Totals.Of(lines));
        }
    }
}
