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
            return dbContext.Users.SingleOrDefault(t => t.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            return dbContext.Users.SingleOrDefault(t => t.Email == email);
        }

        public IEnumerable<User> ListAll()
        {
            return dbContext.Users.ToList();
        }

        public User Remove(int id)
        {
            var dbUser = dbContext.Users.SingleOrDefault(t => t.Id == id);
            if (dbUser == null) return null;

            dbContext.Users.Remove(dbUser);
            dbContext.SaveChanges();
            return dbUser;
        }

        public User UpdateEmail(int id, string newEmail)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return null;

            user.Email = newEmail;
            dbContext.SaveChanges();
            return user;
        }

        public User UpdateName(int id, string newName)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return null;

            user.Name = newName;
            dbContext.SaveChanges();
            return user;
        }

        public User UpdatePassword(int id, string newPassword)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id);

            if (user == null) return null;

            user.Password = newPassword;
            dbContext.SaveChanges();
            return user;
        }
    }
}
