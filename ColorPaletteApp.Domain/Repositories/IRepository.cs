using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Repositories
{
    public interface IRepository<T> where T : class
    {
        public T GetById(int id);
        public IEnumerable<T> ListAll();
        public void Add(T entity);
        public T Remove(int id);
    }
}
