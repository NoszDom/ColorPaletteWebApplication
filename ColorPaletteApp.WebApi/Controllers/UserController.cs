using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
using ColorPaletteApp.Domain.Services;
using ColorPaletteApp.Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPaletteApp.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService service;

        public UserController(UserService service)
        {
            this.service = service;
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserDto>>> List()
        {
            return Ok(await service.GetUsers());
        }

        [HttpGet]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> GetById([FromRoute] int id)
        {
            var result = await service.GetById(id);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> Add([FromBody] RegisterUserDto user)
        {
            if (String.IsNullOrEmpty(user.Name) || 
                String.IsNullOrEmpty(user.Name) || 
                String.IsNullOrEmpty(user.Password)) return BadRequest();

            var result = await service.Add(user);
            if (result == null) return BadRequest();
            else return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<LoggedInUserDto>> Login([FromBody] UserLoginDto user)
        {
            LoggedInUserDto result = await service.Login(user);

            if (result == null) return BadRequest(result);
            else return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> Remove([FromRoute] int id)
        {
            var result = await service.Remove(id);
            if (result == null) return NotFound();
            else return NoContent();
        }

        [HttpPut]
        [Authorize]
        [Route("edit/name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> EditName([FromBody] UserNameUpdateDto user)
        {
            var result = await service.UpdateName(user);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Route("edit/email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UserDto>> EditEmail([FromBody] UserEmailUpdateDto user)
        {
            var result = await service.UpdateEmail(user);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPut]
        [Authorize]
        [Route("edit/password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<UserDto>> EditPassword([FromBody] UserPasswordUpdateDto user)
        {
            var result = await service.UpdatePassword(user);
            if (result == null) return BadRequest();
            else return Ok(result);
        }
    }
}
