using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface ISaveRepository : IRepository<Models.Save>
    {
        public Task<IEnumerable<Models.Save>> ListSavesByPalette(int paletteId);
        public Task<IEnumerable<Models.Save>> ListSavesByUser(int userId);
        public Task<bool> IsPaletteSavedByUser(int paletteId, int userId);
        public Task RemoveAllSavesForPalette(int paletteId);
        public Task<Models.Save> Remove(int paletteId, int userId);
    }
}
