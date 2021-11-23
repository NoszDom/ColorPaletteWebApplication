using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        public Task<T> GetById(int id);
        public Task<IEnumerable<T>> ListAll();
        public Task Add(T entity);
        public Task<T> Remove(int id);
    }
}
