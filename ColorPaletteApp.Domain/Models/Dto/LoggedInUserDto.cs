using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Models.Dto
{
    public class LoggedInUserDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
