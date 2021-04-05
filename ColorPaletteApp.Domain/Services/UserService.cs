using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
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

        public IEnumerable<UserDto> GetUsers() {
            var result = repository.ListAll();
            var list = new List<UserDto>();

            foreach (var user in result) {
                list.Add(new UserDto() {
                    ID = user.ID,
                    Name = user.Name,
                    Email = user.Email,
                });
            }

            return list;
        }

        public UserDto GetById(int id)
        {
            var result = repository.GetById(id);
            if (result == null) return null;
            else return (new UserDto() { ID = result.ID, Name = result.Name, Email = result.Email });
        }

        public UserDto Add(User user) 
        {
            repository.Add(new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            }) ;
           return (new UserDto() { Name = user.Name, Email=user.Email,});
        }

        public UserDto Remove(int id)
        {
           var result =  repository.Remove(id);
            if (result == null) return null;
            else return (new UserDto() { ID = result.ID, Name = result.Name, Email = result.Email });
        }

        public UserDto UpdateName(UserNameUpdateDto user) {
            var result = repository.UpdateName(user.ID, user.Name);
            if (result == null) return null;
            else return (new UserDto() { ID = result.ID, Name = result.Name, Email = result.Email});
        }

        public UserDto UpdateEmail(UserEmailUpdateDto user)
        {
            var result = repository.UpdateEmail(user.ID, user.Email);
            if (result == null) return null;
            else return (new UserDto() { ID = result.ID, Name = result.Name, Email = result.Email });
        }

        public UserDto UpdatePassword(UserPasswordUpdateDto user)
        {
            var dbUser = repository.GetById(user.ID);

            if (!BCrypt.Net.BCrypt.Verify(user.OldPassword, dbUser.Password)) return null;

            var result = repository.UpdatePassword(user.ID, user.NewPassword);
            if (result == null) return null;
            else return (new UserDto() { ID = result.ID, Name = result.Name, Email = result.Email });
        }
    }
}
