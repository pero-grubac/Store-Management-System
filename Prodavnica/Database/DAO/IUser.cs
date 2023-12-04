using Prodavnica.Database.DTO;

namespace Prodavnica.Database.DAO
{
    public interface IUser
    {
        DTO.User GetUser(string username, string password);
        void ChangePassword(ref User user, string newPassword);
        User FindById(int id);
        void ChangeLanguage(ref User user, int idLanguage);
        void SaveUser(User user);
        void CreateUser(User user);
        List<User> GetAll(int id);
        void DeleteUser(int id);
    }
}
