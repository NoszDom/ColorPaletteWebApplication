using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Infrastructure.Repositories
{
    public class SaveRepository : ISaveRepository
    {
        private readonly AppDbContext dbContext;
        public SaveRepository(AppDbContext context)
        {
            dbContext = context;
        }
        public async Task Add(Save entity)
        {
            var dbSave = await dbContext.Saves.SingleOrDefaultAsync(s => s.UserId == entity.UserId && s.ColorPaletteId == entity.ColorPaletteId);
            if (dbSave == null) {
                await dbContext.Saves.AddAsync(entity);
            }
            else
            {
                dbSave.IsDeleted = false;
            }
            await dbContext.SaveChangesAsync();
        }

        public async Task<Save> GetById(int id)
        {
            return await dbContext.Saves.SingleOrDefaultAsync(t => t.Id == id && !t.IsDeleted);
        }

        public async Task<IEnumerable<Save>> ListSavesByPalette(int paletteId)
        {
            return await dbContext.Saves.Where(s => s.ColorPaletteId == paletteId && !s.IsDeleted).ToListAsync();
        }

        public async Task<IEnumerable<Save>> ListAll()
        {
            return await dbContext.Saves.Where(s => !s.IsDeleted).ToListAsync();
        }

        public async Task<Save> Remove(int id)
        {
            var dbSave = await dbContext.Saves.SingleOrDefaultAsync(t => t.Id == id);
            if (dbSave == null) return null;

            dbSave.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return dbSave;
        }

        public async Task<IEnumerable<Save>> ListSavesByUser(int userId)
        {
            return await dbContext.Saves.Where(s => s.UserId == userId && !s.IsDeleted).ToListAsync();
        }

        public async Task<bool> IsPaletteSavedByUser(int paletteId, int userId)
        {
            var result = await dbContext.Saves.SingleOrDefaultAsync(s => s.UserId == userId && s.ColorPaletteId == paletteId && !s.IsDeleted);
            return (result != null);   
        }

        public async Task RemoveAllSavesForPalette(int paletteId)
        {
            var deletable = await dbContext.Saves.Where(s => s.ColorPaletteId == paletteId).ToListAsync();
            if (deletable == null) return;

            foreach (var item in deletable) {
                item.IsDeleted = true;
            }
            await dbContext.SaveChangesAsync();
            return;
        }

        public async Task<Save> Remove(int paletteId, int userId)
        {
            var dbSave = await dbContext.Saves.SingleOrDefaultAsync(s => s.UserId == userId && s.ColorPaletteId == paletteId);
            if (dbSave == null) return null;

            dbSave.IsDeleted = true;
            await dbContext.SaveChangesAsync();
            return dbSave;
        }
    }
}
