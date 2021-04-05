using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models.Dto
{
    public class ColorPaletteDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Colors { get; set; }
        public int CreatorId { get; set; }
        public string CreatorName { get; set; }
        public int Saves { get; set; }
        public bool SavedByCurrentUser { get; set; }
    }
}
