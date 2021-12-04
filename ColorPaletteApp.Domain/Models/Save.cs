using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models
{
    public class Save
    {
        public Save()
        {
            IsDeleted = false;
        }

        public int Id { get; set; }
        public bool IsDeleted { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public int ColorPaletteId { get; set; }
        public virtual ColorPalette ColorPalette{ get; set; }
    }
}
