using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface ISaveRepository : IRepository<Models.Save>
    {
        public IEnumerable<Models.Save> ListSavesByPalette(int palettId);
        public IEnumerable<Models.Save> ListSavesByUser(int userId);
        public bool IsPaletteSavedByUser(int palettId, int userId);

    }
}
