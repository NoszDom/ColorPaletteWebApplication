using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models
{
    public class ColorPalette
    {
        public ColorPalette()
        {
            IsDeleted = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colors { get; set; }
        public bool IsDeleted { get; set; }
        public int CreatorId { get; set; }
        public virtual User Creator { get; set; }
        public virtual List<Save> Saves { get; set; }
    }
}
