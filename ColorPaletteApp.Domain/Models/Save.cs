using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models
{
    public class Save
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ColorPaletteID { get; set; }
    }
}
