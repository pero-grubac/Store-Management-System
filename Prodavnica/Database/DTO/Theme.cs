using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prodavnica.Database.DTO
{
    public class Theme
    {
        public int Id {  get; set; }
        public string ColorName { get; set; }
        public string FontName { get; set; }
        public int Size { get; set; }
        public string FontStyle { get; set; }
        public bool IsStrikeout { get; set; }
        public bool IsUnderline { get; set; }

    }
}
