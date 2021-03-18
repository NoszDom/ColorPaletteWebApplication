using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<Models.User>
    {
        public Models.User Update(Models.User user);
    }
}
