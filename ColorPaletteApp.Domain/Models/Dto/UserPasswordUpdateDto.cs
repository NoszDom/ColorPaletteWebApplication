using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models.Dto
{
    public class UserPasswordUpdateDto
    {
        public int ID { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
