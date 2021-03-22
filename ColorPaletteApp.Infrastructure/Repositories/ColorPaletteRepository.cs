﻿using ColorPaletteApp.Domain.Models;
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
            return dbContext.ColorPalettes.SingleOrDefault(t => t.ID == id);
        }

        public IEnumerable<ColorPalette> ListAll()
        {
            return dbContext.ColorPalettes.ToList();
        }

        public ColorPalette Remove(int id)
        {
            var dbPalette = dbContext.ColorPalettes.SingleOrDefault(t => t.ID == id);
            if (dbPalette == null) return null;

            dbContext.ColorPalettes.Remove(dbPalette);
            dbContext.SaveChanges();
            return dbPalette;
        }

        public ColorPalette Update(ColorPalette colorPalette)
        {
            throw new NotImplementedException();
        }
    }
}