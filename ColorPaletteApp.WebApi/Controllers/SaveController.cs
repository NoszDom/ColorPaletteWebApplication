using ColorPaletteApp.Domain.Models;
using ColorPaletteApp.Domain.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPaletteApp.WebApi.Controllers
{
    [Route("api/saves")]
    [ApiController]
    public class SaveController : Controller
    {
        private readonly SaveService service;

        public SaveController(SaveService service)
        {
            this.service = service;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Save>>> List()
        {
            return Ok(await service.GetSaves());
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Save>> GetById([FromRoute] int id)
        {
            var result = await service.GetById(id);
            if (result == null) return NotFound();
            else return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Add([FromBody] Save save)
        {
            var result = await service.Add(save);
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Remove([FromRoute] int id)
        {
            var result = await service .Remove(id);
            if (result == null) return NotFound();
            else return NoContent();
        }

        [HttpDelete]
        [Route("{paletteId}/{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> Remove([FromRoute] int paletteId, [FromRoute] int userId)
        {
            var result = await service.Remove(paletteId, userId);
            if (result == null) return NotFound();
            else return NoContent();
        }
    }
}
