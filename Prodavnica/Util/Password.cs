using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Util
{
    public class Password
    {
        private static string userName;
        private static string password;
        public static string HashValue(string newPassword)
        {
            string salt = BCrypt.Net.BCrypt.GenerateSalt();
            return BCrypt.Net.BCrypt.HashPassword(newPassword, salt);
        }
        public static bool Verify(string password, string hashedPassword )
        {      
            if (hashedPassword == null) return false;
            bool result;
            try
            {
                result = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
            }
            catch (Exception e)
            {
                return false;
            }
            return result;

        }
    }
    
}
