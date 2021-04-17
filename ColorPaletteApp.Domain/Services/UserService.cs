using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
using ColorPaletteApp.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;

namespace ColorPaletteApp.Domain.Services
{
    public class UserService
    {
        private readonly IUserRepository repository;
        private readonly IConfiguration configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            this.repository = repository;
            this.configuration = configuration;
        }

        public IEnumerable<UserDto> GetUsers() {
            var result = repository.ListAll();
            var list = new List<UserDto>();

            foreach (var user in result) {
                list.Add(new UserDto() {
                    Id = user.Id,
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
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }


        public string Login(UserLoginDto user) {
            var result = repository.GetUserByEmail(user.Email);
            if (result == null) return "no_user";
            else {
                if (!BCrypt.Net.BCrypt.Verify(user.Password, result.Password)) return "wrong_password";
                else {
                    var jwtTokenHandler = new JwtSecurityTokenHandler();
                    var config = configuration.GetSection("TokenKey").Value;
                    var key = Encoding.ASCII.GetBytes(config);
                    var securityTokenDescriptor = new SecurityTokenDescriptor() {
                        Subject = new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.NameIdentifier, result.Id.ToString()),
                            new Claim(ClaimTypes.Email, result.Email),
                            new Claim(ClaimTypes.Name, result.Name),
                        }),
                        Expires = DateTime.Now.AddDays(1),
                        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
                    };

                    var token = jwtTokenHandler.CreateToken(securityTokenDescriptor);
                    return jwtTokenHandler.WriteToken(token);
                }
            }
        } 

        public UserDto Add(User user) 
        {
            if (repository.GetUserByEmail(user.Email) != null) return null;
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
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }

        public UserDto UpdateName(UserNameUpdateDto user) {
            var result = repository.UpdateName(user.Id, user.Name);
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email});
        }

        public UserDto UpdateEmail(UserEmailUpdateDto user)
        {
            var result = repository.UpdateEmail(user.Id, user.Email);
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }

        public UserDto UpdatePassword(UserPasswordUpdateDto user)
        {
            var dbUser = repository.GetById(user.Id);

            if (!BCrypt.Net.BCrypt.Verify(user.OldPassword, dbUser.Password)) return null;

            var result = repository.UpdatePassword(user.Id, BCrypt.Net.BCrypt.HashPassword(user.NewPassword));
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }
    }
}
