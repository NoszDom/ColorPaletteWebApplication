using ColorPaletteApp.Domain.Models.ColorPalettes;
using ColorPaletteApp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Infrastructure.Repositories.ColorPalettes
{
    public class ColorPaletteRepository : IRepository<ColorPalette>
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
            return dbContext.ColorPalettes.SingleOrDefault(t => t.ID == id);
        }

        public IEnumerable<ColorPalette> ListAll()
        {
            return dbContext.ColorPalettes.ToList();
        }

        public bool Remove(int id)
        {
            var dbPalette = dbContext.ColorPalettes.SingleOrDefault(t => t.ID == id);
            if (dbPalette == null) return false;

            dbContext.ColorPalettes.Remove(dbPalette);
            dbContext.SaveChanges();
            return true;
        }

        public void Update(ColorPalette entity)
        {
            throw new NotImplementedException();
        }
    }
}
