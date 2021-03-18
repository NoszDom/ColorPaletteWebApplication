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
            return dbContext.Saves.SingleOrDefault(t => t.ID == id);
        }

        public IEnumerable<Save> ListAll()
        {
            return dbContext.Saves.ToList();
        }

        public Save Remove(int id)
        {
            var dbSave = dbContext.Saves.SingleOrDefault(t => t.ID == id);
            if (dbSave == null) return null;

            dbContext.Saves.Remove(dbSave);
            dbContext.SaveChanges();
            return dbSave;
        }
    }
}
