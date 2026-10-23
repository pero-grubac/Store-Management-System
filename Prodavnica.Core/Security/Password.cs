namespace Prodavnica.Security
{
    public static class Password
    {
        public const int WorkFactor = 11;

        public static string HashValue(string newPassword)
        {
            return BCrypt.Net.BCrypt.HashPassword(newPassword, WorkFactor);
        }

        public static bool Verify(string password, string? hashedPassword)
        {
            if (string.IsNullOrEmpty(hashedPassword)) return false;
            try
            {
                return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (BCrypt.Net.SaltParseException)
            {
                // A value that is not a BCrypt hash never matches.
                return false;
            }
        }
    }
}
