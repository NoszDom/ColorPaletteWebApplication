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

        public IEnumerable<Save> GetSaves()
        {
            return repository.ListAll();
        }

        public Save GetById(int id)
        {
            return repository.GetById(id);
        }

        public Save Add(Save Save)
        {
            repository.Add(Save);
            return Save;
        }

        public Save Remove(int id)
        {
            return repository.Remove(id);
        }

        public Save Remove(int userId, int paletteId)
        {
            return repository.Remove(userId, paletteId);
        }
    }
}
