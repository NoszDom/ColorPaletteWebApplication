using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Services
{
    public class SaveService
    {
        private readonly ISaveRepository repository;

        public SaveService(ISaveRepository repository)
        {
            this.repository = repository;
        }

        public async Task<IEnumerable<Save>> GetSaves()
        {
            return await repository.ListAll();
        }

        public async Task<Save> GetById(int id)
        {
            return await repository.GetById(id);
        }

        public async Task<Save> Add(Save save)
        {
            var result = await repository.Add(save);
            if (result) return save;
            else return null;
        }

        public async Task<Save> Remove(int id)
        {
            return await repository .Remove(id);
        }

        public async Task<Save> Remove(int userId, int paletteId)
        {
            return await repository.Remove(userId, paletteId);
        }
    }
}
