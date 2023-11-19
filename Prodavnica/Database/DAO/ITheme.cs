using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface ITheme
    {
        Theme FindById(int  id); 
        void ChangeTheme(Theme newTheme);
        Color GetColor(Theme theme);
        Font GetFont(Theme theme);
    }
}
