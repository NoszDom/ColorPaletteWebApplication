using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
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

        public User Remove(int id)
        {
            var dbUser = dbContext.Users.SingleOrDefault(t => t.ID == id);
            if (dbUser == null) return null;

            dbContext.Users.Remove(dbUser);
            dbContext.SaveChanges();
            return dbUser;
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
