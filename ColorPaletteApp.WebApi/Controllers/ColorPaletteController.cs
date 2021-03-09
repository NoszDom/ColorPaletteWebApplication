using ColorPaletteApp.Domain.Models.ColorPalettes;
using ColorPaletteApp.Infrastructure.Repositories;
using ColorPaletteApp.Infrastructure.Repositories.ColorPalettes;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPaletteApp.WebApi.Controllers
{
    [Route("api/colorpalettes")]
    [ApiController]
    public class ColorPaletteController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly ColorPaletteRepository repository;

        public ColorPaletteController(AppDbContext context)
        {
            dbContext = context;
            repository = new ColorPaletteRepository(dbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<ColorPalette>> List()
        {
            return Json(repository.ListAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<ColorPalette> GetById([FromRoute] int id)
        {
            var dbPalette = Json(repository.GetById(id));
            if (dbPalette == null) return NotFound();
            return dbPalette;
        }

        [HttpPost]
        public ActionResult Add([FromBody] ColorPalette palette)
        {
            repository.Add(palette);
            return Ok();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Remove([FromRoute] int id)
        {
            bool result = repository.Remove(id);
            if (result == false) return NotFound();
            else return NoContent();
        }
    }
}
