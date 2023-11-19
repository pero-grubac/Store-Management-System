using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DTO
{
    public class Theme
    {
        public int id {  get; set; }
        public string colorName { get; set; }
        public string fontName { get; set; }
        public int size { get; set; }
        public string fontStyle { get; set; }
        public bool isStrikeout { get; set; }
        public bool isUnderline { get; set; }

    }
}
