using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models.Dto
{
    public class CreateColorPaletteDto
    {
        public string Name { get; set; }
        public string Colors { get; set; }
        public int CreatorId { get; set; }
    }
}
