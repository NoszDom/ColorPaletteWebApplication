using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Services
{
    public class ColorPaletteService
    {
        private readonly IColorPaletteRepository repository;

        public ColorPaletteService(IColorPaletteRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<ColorPalette> GetColorPalettes()
        {
            return repository.ListAll();
        }

        public ColorPalette GetById(int id)
        {
            return repository.GetById(id);
        }

        public ColorPalette Add(ColorPalette ColorPalette)
        {
            repository.Add(ColorPalette);
            return ColorPalette;
        }

        public ColorPalette Remove(int id)
        {
            return repository.Remove(id);
        }
    }
}
