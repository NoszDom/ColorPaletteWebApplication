using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Models.Dto;
using ColorPaletteApp.Domain.Services;
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
        public ActionResult<IEnumerable<ColorPaletteDto>> List([FromQuery] string order, [FromQuery] string sortBy, [FromQuery] string sortValue)
        {
          return Ok(service.GetColorPalettes(order, sortBy, sortValue));
        }

        [HttpGet]
        [Route("{user}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<ColorPaletteDto>> List([FromRoute]int user, [FromQuery] int? creator,
            [FromQuery] string order, [FromQuery] string sortBy, [FromQuery] string sortValue)
        {
            int creatorId = creator ?? -1;
            if (creatorId == -1) return Ok(service.GetColorPalettes(user, order, sortBy, sortValue));
            else return Ok(service.GetPalettesByUser(user, creatorId, order, sortBy, sortValue));
        }

        [HttpGet]
        [Route("{user}/palette/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ColorPaletteDto> GetById([FromRoute] int user, [FromRoute] int id)
        {
            var result = service.GetById(user, id);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpGet]
        [Route("{user}/saved")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ColorPaletteDto> GetSavedByUser([FromRoute] int user, [FromQuery] string order,
            [FromQuery] string sortBy, [FromQuery] string sortValue)
        {
            return Ok(service.GetPalettesSavedByUser(user, order, sortBy, sortValue));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult Add([FromBody] ColorPalette palette)
        {
            var result = service.Add(palette);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult Remove([FromRoute] int id)
        {
            var result = service.Remove(id);
            if (result == null) return NotFound();
            else return NoContent();
        }
    }
}
