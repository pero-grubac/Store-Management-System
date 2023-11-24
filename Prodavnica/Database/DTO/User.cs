using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;

namespace Prodavnica.Database.DTO
{
    public class User
    {
        public int Id { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String UserName { get; set; }
        public String PhoneNumber { get; set; }
        public Boolean IsAdmin { get; set; }
        public int IdTheme { get; set; }
        public int IdLangugae { get; set; }

    }
}
