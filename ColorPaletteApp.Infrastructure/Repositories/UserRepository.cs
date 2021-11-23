using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public async Task Add(User entity)
        {
            await dbContext .Users.AddAsync(entity);
            await dbContext .SaveChangesAsync();
        }

        public async Task<User> GetById(int id)
        {
            return await dbContext.Users
                .Include(u => u.CreatedPalettes)
                .Include(u => u.Saves)
                .ThenInclude(s => s.ColorPalette)
                .ThenInclude(cp => cp.Creator)
                .Include(cp => cp.Saves)
                .SingleOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await dbContext.Users
                .Include(u => u.CreatedPalettes)
                .Include(u => u.Saves)
                .ThenInclude(s => s.ColorPalette)
                .ThenInclude(cp => cp.Creator)
                .Include(cp => cp.Saves)
                .SingleOrDefaultAsync(t => t.Email == email && !t.IsDeleted);
        }

        public async Task<IEnumerable<User>> ListAll()
        {
            return await dbContext.Users
                .Include(u => u.CreatedPalettes)
                .Include(u => u.Saves)
                .ThenInclude(s => s.ColorPalette)
                .ThenInclude(cp => cp.Creator)
                .Include(cp => cp.Saves)
                .Where(t=> !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<User> Remove(int id)
        {
            var dbUser = await dbContext.Users.SingleOrDefaultAsync(t => t.Id == id);
            if (dbUser == null) return null;

            dbUser.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return dbUser;
        }

        public async Task<User> UpdateEmail(int id, string newEmail)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null) return null;

            user.Email = newEmail;
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdateName(int id, string newName)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null) return null;

            user.Name = newName;
            await dbContext.SaveChangesAsync();
            return user;
        }

        public async Task<User> UpdatePassword(int id, string newPassword)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);

            if (user == null) return null;

            user.Password = newPassword;
            await dbContext.SaveChangesAsync();
            return user;
        }
    }
}
