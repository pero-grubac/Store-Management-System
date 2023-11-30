using Prodavnica.Database.DTO;

namespace Prodavnica.Database.DAO
{
    public interface ITheme
    {
        Theme FindById(int id);
        void ChangeTheme(Theme newTheme);
        Color GetColor(Theme theme);
        Font GetFont(Theme theme);
        void CreateTheme(ref Theme theme);
    }
}
