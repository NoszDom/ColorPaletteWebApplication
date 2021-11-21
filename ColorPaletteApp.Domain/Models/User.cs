using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models
{
    public class User
    {
        public User()
        {
            IsDeleted = false;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsDeleted { get; set;  }
        public virtual IList<Save> Saves { get; set; }
        public virtual IList<ColorPalette> CreatedPalettes { get; set; }
    }
}
