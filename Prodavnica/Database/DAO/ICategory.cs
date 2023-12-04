using Prodavnica.Database.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DAO
{
    public interface ICategory
    {
        public List<Category> GetAll();
        void Create(Category category);
        void Update(Category category);
        void Delete(Category category);
    }
}
