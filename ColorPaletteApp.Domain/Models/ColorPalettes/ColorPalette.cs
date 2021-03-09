using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models.ColorPalettes
{
    public class ColorPalette
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Colors { get; set; }
        public int? CreatorID { get; set; }
    }
}
