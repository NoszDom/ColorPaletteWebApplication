using ColorPaletteApp.Domain.Models;
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
        public ActionResult<IEnumerable<User>> List()
        {
            return Ok(service.GetUsers());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            var result = service.GetById(id);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Add([FromBody] User user)
        {
            var result = service.Add(user);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<User> Remove([FromRoute] int id)
        {
            var result = service.Remove(id);
            if (result == null) return NotFound();
            else return NoContent();
        }
    }
}
