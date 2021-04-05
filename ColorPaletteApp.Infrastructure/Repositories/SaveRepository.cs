using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
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
        public void Add(Save entity)
        {
            dbContext.Saves.Add(entity);
            dbContext.SaveChanges();
        }

        public Save GetById(int id)
        {
            return dbContext.Saves.SingleOrDefault(t => t.Id == id);
        }

        public IEnumerable<Save> ListSavesByPalette(int paletteId)
        {
            return dbContext.Saves.Where(s => s.ColorPaletteID == paletteId).ToList();
        }

        public IEnumerable<Save> ListAll()
        {
            return dbContext.Saves.ToList();
        }

        public Save Remove(int id)
        {
            var dbSave = dbContext.Saves.SingleOrDefault(t => t.Id == id);
            if (dbSave == null) return null;

            dbContext.Saves.Remove(dbSave);
            dbContext.SaveChanges();
            return dbSave;
        }

        public IEnumerable<Save> ListSavesByUser(int userId)
        {
            return dbContext.Saves.Where(s => s.UserID == userId).ToList();
        }

        public bool IsPaletteSavedByUser(int paletteId, int userId)
        {
            var result = dbContext.Saves.SingleOrDefault(s => s.UserID == userId && s.ColorPaletteID == paletteId);
            return (result != null);   
        }

        public void RemoveAllSavesForPalette(int paletteId)
        {
            var deletable = dbContext.Saves.Where(s => s.ColorPaletteID == paletteId).ToList();
            if (deletable == null) return;

            foreach (var item in deletable) {
                dbContext.Saves.Remove(item);
            }
            dbContext.SaveChanges();
            return;
        }

        public Save Remove(int paletteId, int userId)
        {
            var dbSave = dbContext.Saves.SingleOrDefault(s => s.UserID == userId && s.ColorPaletteID == paletteId);
            if (dbSave == null) return null;

            dbContext.Saves.Remove(dbSave);
            dbContext.SaveChanges();
            return dbSave;
        }
    }
}
