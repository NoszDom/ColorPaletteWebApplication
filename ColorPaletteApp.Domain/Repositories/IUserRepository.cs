using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface IUserRepository : IRepository<Models.User>
    {
        public Task<Models.User> UpdateName(int id, string newName);
        public Task<Models.User> UpdateEmail(int id, string newEmail);
        public Task<Models.User> UpdatePassword(int id, string newPassword);
        public Task<Models.User> GetUserByEmail(string email);
    }
}
