using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
using ColorPaletteApp.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPaletteApp.WebApi.Controllers
{
    [Route("api/colorpalettes")]
    [ApiController]
    public class ColorPaletteController : ControllerBase
    {
        private readonly ColorPaletteService service;

        public ColorPaletteController(ColorPaletteService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ColorPaletteDto>>> List([FromQuery] string order, [FromQuery] string sortBy, [FromQuery] string sortValue)
        {
          return Ok(await service.GetColorPalettes(order, sortBy, sortValue));
        }

        [HttpGet]
        [Authorize]
        [Route("{user}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<ColorPaletteDto>>> List([FromRoute]int user, [FromQuery] int? creator,
            [FromQuery] string order, [FromQuery] string sortBy, [FromQuery] string sortValue)
        {
            int creatorId = creator ?? -1;
            if (creatorId == -1) return Ok(await service.GetColorPalettes(user, order, sortBy, sortValue));
            else return Ok(await service.GetPalettesByUser(user, creatorId, order, sortBy, sortValue));
        }

        [HttpGet]
        [Authorize]
        [Route("{user}/palette/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ColorPaletteDto>> GetById([FromRoute] int user, [FromRoute] int id)
        {
            var result = await service.GetById(user, id);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpGet]
        [Authorize]
        [Route("{user}/saved")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<ColorPaletteDto>> GetSavedByUser([FromRoute] int user, [FromQuery] string order,
            [FromQuery] string sortBy, [FromQuery] string sortValue)
        {
            return Ok(await service.GetPalettesSavedByUser(user, order, sortBy, sortValue));
        }

     
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add([FromBody] CreateColorPaletteDto palette)
        {
            var result = await service.Add(palette);

            if (result == null) return BadRequest();
            else return Ok(result);
        }

        [HttpDelete]
        [Authorize]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await service.Remove(id);
            if (result == null) return NotFound();
            else return NoContent();
        }
    }
}
