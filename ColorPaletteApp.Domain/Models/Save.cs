using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models
{
    public class Save
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public int ColorPaletteID { get; set; }
    }
}
