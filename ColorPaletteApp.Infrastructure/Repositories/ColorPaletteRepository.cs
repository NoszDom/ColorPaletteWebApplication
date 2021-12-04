using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPaletteApp.Infrastructure.Repositories
{
    public class ColorPaletteRepository : IColorPaletteRepository
    {
        private readonly AppDbContext dbContext; 
        public ColorPaletteRepository(AppDbContext context) {
            dbContext = context;
        }

        public async Task<bool> Add(ColorPalette entity)
        {
            await dbContext.ColorPalettes.AddAsync(entity);
            var result = await dbContext.SaveChangesAsync();
            return result != 0;
        }

        public async Task<ColorPalette> GetById(int id)
        {
            return  await dbContext.ColorPalettes
                .Include(c => c.Creator)
                .Include(c => c.Saves)
                .SingleOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<IEnumerable<ColorPalette>> ListAll()
        {
            return await dbContext.ColorPalettes
                .Include(c => c.Creator)
                .Include(c => c.Saves)
                .Where(t => !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<ColorPalette>> ListByUser(int creatorId)
        {
            return await dbContext.ColorPalettes
                .Include(c => c.Creator)
                .Include(c => c.Saves)
                .Where(t => t.CreatorId == creatorId && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<IEnumerable<ColorPalette>> ListNotOwn(int creatorId)
        {
            return await dbContext.ColorPalettes
                .Include(c => c.Creator)
                .Include(c => c.Saves)
                .Where(t => t.CreatorId != creatorId && !t.IsDeleted)
                .ToListAsync();
        }

        public async Task<ColorPalette> Remove(int id)
        {
            var dbPalette = await dbContext.ColorPalettes.SingleOrDefaultAsync(t => t.Id == id);
            if (dbPalette == null) return null;

            dbPalette.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return dbPalette;
        }
    }
}
