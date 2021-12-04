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

        public async Task<IEnumerable<UserDto>> GetUsers()
        {
            var result = await repository.ListAll();
            var list = new List<UserDto>();

            foreach (var user in result)
            {
                list.Add(new UserDto()
                {
                    Id = user.Id,
                    Name = user.Name,
                    Email = user.Email,
                });
            }

            return list;
        }

        public async Task<UserDto> GetById(int id)
        {
            var result = await repository.GetById(id);
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }


        public async Task<LoggedInUserDto> Login(UserLoginDto user)
        {
            var result = await repository.GetUserByEmail(user.Email);

            if (result == null)
            {
                return null;
            }
            else
            {
                if (!BCrypt.Net.BCrypt.Verify(user.Password, result.Password))
                {
                    return null;
                }
                else
                {
                    return (new LoggedInUserDto()
                    {
                        User = new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email },
                        Token = CreateToken(result)
                    });
                }
            }
        }

        private string CreateToken(User user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();
            var config = configuration.GetSection("TokenKey").Value;
            var key = Encoding.ASCII.GetBytes(config);
            var securityTokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[] {
                            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                            new Claim(ClaimTypes.Email, user.Email),
                            new Claim(ClaimTypes.Name, user.Name),
                        }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            var token = jwtTokenHandler.CreateToken(securityTokenDescriptor);
            return jwtTokenHandler.WriteToken(token);
        }

        public async Task<UserDto> Add(RegisterUserDto user)
        {
            if (await repository.GetUserByEmail(user.Email) != null) return null;
            await repository.Add(new User()
            {
                Name = user.Name,
                Email = user.Email,
                Password = BCrypt.Net.BCrypt.HashPassword(user.Password),
            });
            return (new UserDto() { Name = user.Name, Email = user.Email, });
        }

        public async Task<UserDto> Remove(int id)
        {
            var result = await repository.Remove(id);
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }

        public async Task<UserDto> UpdateName(UserNameUpdateDto user)
        {
            var result = await repository.UpdateName(user.Id, user.Name);
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }

        public async Task<UserDto> UpdateEmail(UserEmailUpdateDto user)
        {
            var result = await repository.UpdateEmail(user.Id, user.Email);
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }

        public async Task<UserDto> UpdatePassword(UserPasswordUpdateDto user)
        {
            var dbUser = await repository.GetById(user.Id);

            if (!BCrypt.Net.BCrypt.Verify(user.OldPassword, dbUser.Password)) return null;

            var result = await repository.UpdatePassword(user.Id, BCrypt.Net.BCrypt.HashPassword(user.NewPassword));
            if (result == null) return null;
            else return (new UserDto() { Id = result.Id, Name = result.Name, Email = result.Email });
        }
    }
}
