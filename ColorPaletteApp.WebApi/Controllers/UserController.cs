using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
using ColorPaletteApp.Domain.Services;
using ColorPaletteApp.Infrastructure.Repositories;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<UserDto>> List()
        {
            return Ok(service.GetUsers());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> GetById([FromRoute] int id)
        {
            var result = service.GetById(id);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> Add([FromBody] User user)
        {
            var result = service.Add(user);
            if (result == null) return BadRequest();
            else return Ok(result);
        }

        [HttpPost]
        [Route("login")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<string> Login([FromBody] UserLoginDto user)
        {
            string result = service.Login(user);

            if (result == "no_user" || result == "wrong_password") return BadRequest(result);
            else return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> Remove([FromRoute] int id)
        {
            var result = service.Remove(id);
            if (result == null) return NotFound();
            else return NoContent();
        }

        [HttpPut]
        [Route("edit/name")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> EditName([FromBody] UserNameUpdateDto user)
        {
            var result = service.UpdateName(user);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPut]
        [Route("edit/email")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> EditEmail([FromBody] UserEmailUpdateDto user)
        {
            var result = service.UpdateEmail(user);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPut]
        [Route("edit/password")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> EditPassword([FromBody] UserPasswordUpdateDto user)
        {
            var result = service.UpdatePassword(user);
            if (result == null) return BadRequest();
            else return Ok(result);
        }
    }
}
