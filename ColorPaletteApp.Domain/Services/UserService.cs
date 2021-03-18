using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorPaletteApp.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository repository;

        public UserService(IUserRepository repository)
        {
            this.repository = repository;
        }

        public IEnumerable<User> GetUsers() {
            return repository.ListAll();
        }

        public User GetById(int id)
        {
            return repository.GetById(id);
        }

        public User Add(User user) 
        {
           repository.Add(user);
           return user;
        }

        public User Remove(int id)
        {
           return repository.Remove(id);
        }
    }
}
