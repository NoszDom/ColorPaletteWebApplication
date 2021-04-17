using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<Models.User>
    {
        public Models.User UpdateName(int id, string newName);
        public Models.User UpdateEmail(int id, string newEmail);
        public Models.User UpdatePassword(int id, string newPassword);
        public Models.User GetUserByEmail(string email);
    }
}
