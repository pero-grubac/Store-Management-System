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
        public int id { get; set; }
        public String firstName { get; set; }
        public String lastName { get; set; }
        public String email { get; set; }
        public String password { get; set; }
        public String userName { get; set; }
        public String phoneNumber { get; set; }
        public Boolean isAdmin { get; set; }
        public int idTheme { get; set; }
        public int idLangugae { get; set; }

    }
}
