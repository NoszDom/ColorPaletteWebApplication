using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;


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
            return dbContext.Users
                .Include(u => u.CreatedPalettes)
                .Include(u => u.Saves)
                .ThenInclude(s => s.ColorPalette)
                .ThenInclude(cp => cp.Creator)
                .Include(cp => cp.Saves)
                .SingleOrDefault(t => t.Id == id && !t.IsDeleted);
        }

        public User GetUserByEmail(string email)
        {
            return dbContext.Users
                .Include(u => u.CreatedPalettes)
                .Include(u => u.Saves)
                .ThenInclude(s => s.ColorPalette)
                .ThenInclude(cp => cp.Creator)
                .Include(cp => cp.Saves)
                .SingleOrDefault(t => t.Email == email && !t.IsDeleted);
        }

        public IEnumerable<User> ListAll()
        {
            return dbContext.Users
                .Include(u => u.CreatedPalettes)
                .Include(u => u.Saves)
                .ThenInclude(s => s.ColorPalette)
                .ThenInclude(cp => cp.Creator)
                .Include(cp => cp.Saves)
                .Where(t=> !t.IsDeleted).ToList();
        }

        public User Remove(int id)
        {
            var dbUser = dbContext.Users.SingleOrDefault(t => t.Id == id);
            if (dbUser == null) return null;

            dbUser.IsDeleted = true;
            dbContext.SaveChanges();
            return dbUser;
        }

        public User UpdateEmail(int id, string newEmail)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);

            if (user == null) return null;

            user.Email = newEmail;
            dbContext.SaveChanges();
            return user;
        }

        public User UpdateName(int id, string newName)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);

            if (user == null) return null;

            user.Name = newName;
            dbContext.SaveChanges();
            return user;
        }

        public User UpdatePassword(int id, string newPassword)
        {
            var user = dbContext.Users.FirstOrDefault(u => u.Id == id && !u.IsDeleted);

            if (user == null) return null;

            user.Password = newPassword;
            dbContext.SaveChanges();
            return user;
        }
    }
}
