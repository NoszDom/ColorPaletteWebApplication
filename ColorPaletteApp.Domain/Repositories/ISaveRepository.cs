using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface ISaveRepository : IRepository<Models.Save>
    {
        public IEnumerable<Models.Save> ListSavesByPalette(int paletteId);
        public IEnumerable<Models.Save> ListSavesByUser(int userId);
        public bool IsPaletteSavedByUser(int paletteId, int userId);
        public void RemoveAllSavesForPalette(int paletteId);
        public Models.Save Remove(int paletteId, int userId);
    }
}
