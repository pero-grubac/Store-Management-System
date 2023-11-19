using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface ILanguage
    {
        List<DTO.Language> GetAll();
        DTO.Language GetLanguageById(int id);
        List<string> GetLanguages();
        string GetSelectedLanguageName(int id);
    }
}
