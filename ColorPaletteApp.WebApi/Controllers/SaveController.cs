using ColorPaletteApp.Domain.Models.Interactions;
using ColorPaletteApp.Infrastructure.Repositories;
using ColorPaletteApp.Infrastructure.Repositories.Interactions;
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
        private readonly AppDbContext dbContext;
        private readonly SaveRepository repository;

        public SaveController(AppDbContext context)
        {
            dbContext = context;
            repository = new SaveRepository(dbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<Save>> List()
        {
            return Json(repository.ListAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<Save> GetById([FromRoute] int id)
        {
            var dbSave = Json(repository.GetById(id));
            if (dbSave == null) return NotFound();
            return dbSave;
        }

        [HttpPost]
        public ActionResult Add([FromBody] Save save)
        {
            repository.Add(save);
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
