using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface IUser
    {
        User GetUser(string username, string password);
        void changePassword(User user,string newPassword);
        User FindById(int id);

    }
}
