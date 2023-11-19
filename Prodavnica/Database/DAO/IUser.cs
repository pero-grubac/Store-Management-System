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
        DTO.User GetUser(string username, string password);
        void ChangePassword(ref User user,string newPassword);
        User FindById(int id);
        void ChangeLanguage(ref User user,int idLanguage);
        void SaveUser(User user);
    }
}
