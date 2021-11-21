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
    public class ColorPaletteRepository : IColorPaletteRepository
    {
        private readonly AppDbContext dbContext; 
        public ColorPaletteRepository(AppDbContext context) {
            dbContext = context;
        }

        public void Add(ColorPalette entity)
        {
            dbContext.ColorPalettes.Add(entity);
            dbContext.SaveChanges();
        }

        public ColorPalette GetById(int id)
        {
            return dbContext.ColorPalettes.SingleOrDefault(t => t.Id == id && !t.IsDeleted);
        }

        public IEnumerable<ColorPalette> ListAll()
        {
            return dbContext.ColorPalettes.Where(t => !t.IsDeleted).ToList();
        }

        public IEnumerable<ColorPalette> ListByUser(int creatorId)
        {
            return dbContext.ColorPalettes.Where(t => t.CreatorID == creatorId && !t.IsDeleted).ToList();
        }

        public IEnumerable<ColorPalette> ListNotOwn(int creatorId)
        {
            return dbContext.ColorPalettes.Where(t => t.CreatorID != creatorId && !t.IsDeleted).ToList();
        }

        public ColorPalette Remove(int id)
        {
            var dbPalette = dbContext.ColorPalettes.SingleOrDefault(t => t.Id == id);
            if (dbPalette == null) return null;

            dbPalette.IsDeleted = true;
            dbContext.SaveChanges();
            return dbPalette;
        }
    }
}
