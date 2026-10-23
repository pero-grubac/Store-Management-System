using Prodavnica.Security;
using Prodavnica.Services;

namespace Prodavnica.Tests.Unit
{
    public class ThemeFormatTests
    {
        [Fact]
        public void Parses_the_stored_argb_format()
        {
            Assert.Equal((255, 0, 128, 192), ThemeFormat.ParseArgb("255,0,128,192"));
            Assert.Equal((255, 1, 2, 3), ThemeFormat.ParseArgb(" 255 , 1 ,2, 3 "));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("0,128,128")]
        [InlineData("255,0,128,128,1")]
        [InlineData("255,0,abc,128")]
        [InlineData("255,0,256,128")]
        [InlineData("255,-1,0,0")]
        public void Rejects_malformed_values(string? value)
        {
            Assert.Null(ThemeFormat.ParseArgb(value));
        }

        [Fact]
        public void Format_and_parse_round_trip()
        {
            string stored = ThemeFormat.FormatArgb(255, 10, 20, 30);

            Assert.Equal("255,10,20,30", stored);
            Assert.Equal((255, 10, 20, 30), ThemeFormat.ParseArgb(stored));
        }

        [Fact]
        public void The_default_colour_is_valid()
        {
            Assert.NotNull(ThemeFormat.ParseArgb(ThemeFormat.DefaultColor));
        }
    }

    public class LanguageCodesTests
    {
        [Theory]
        [InlineData("English", "en")]
        [InlineData("Srpski(Latinica)", "sr-Latn-RS")]
        [InlineData("Српски(Ћирилица)", "sr-Cyrl-RS")]
        [InlineData("Deutsch", "en")]
        [InlineData(null, "en")]
        public void Maps_language_names_to_cultures(string? name, string expected)
        {
            Assert.Equal(expected, LanguageCodes.FromName(name));
        }
    }

    public class PasswordTests
    {
        [Fact]
        public void A_hash_verifies_only_the_original_password()
        {
            string hash = Password.HashValue("kasir123");

            Assert.NotEqual("kasir123", hash);
            Assert.True(Password.Verify("kasir123", hash));
            Assert.False(Password.Verify("Kasir123", hash));
        }

        [Fact]
        public void Each_hash_has_its_own_salt()
        {
            Assert.NotEqual(Password.HashValue("same"), Password.HashValue("same"));
        }

        [Fact]
        public void Uses_the_configured_work_factor()
        {
            Assert.Contains($"${Password.WorkFactor}$", Password.HashValue("x"));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("plain-text-password")]
        public void A_missing_or_invalid_hash_never_matches(string? stored)
        {
            Assert.False(Password.Verify("plain-text-password", stored));
        }
    }
}
