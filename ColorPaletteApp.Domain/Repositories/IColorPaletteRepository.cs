using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface IColorPaletteRepository : IRepository<Models.ColorPalette>
    {
        public Task<IEnumerable<Models.ColorPalette>> ListByUser(int creatorId);
        public Task<IEnumerable<Models.ColorPalette>> ListNotOwn(int creatorId);
    }
}
