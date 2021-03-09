using ColorPaletteApp.Domain.Models.Users;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Infrastructure.Repositories.Users
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext dbContext;
        public UserRepository(AppDbContext context)
        {
            dbContext = context;
        }
        public void Add(User entity)
        {
            dbContext.Users.Add(entity);
            dbContext.SaveChanges();
        }

        public User GetById(int id)
        {
            return dbContext.Users.SingleOrDefault(t => t.ID == id);
        }

        public IEnumerable<User> ListAll()
        {
            return dbContext.Users.ToList();
        }

        public bool Remove(int id)
        {
            var dbUser = dbContext.Users.SingleOrDefault(t => t.ID == id);
            if (dbUser == null) return false;

            dbContext.Users.Remove(dbUser);
            dbContext.SaveChanges();
            return true;
        }

        public void Update(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
