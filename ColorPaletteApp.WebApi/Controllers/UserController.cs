using ColorPaletteApp.Domain.Models.Users;
using ColorPaletteApp.Infrastructure.Repositories;
using ColorPaletteApp.Infrastructure.Repositories.Users;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ColorPaletteApp.WebApi.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly AppDbContext dbContext;
        private readonly UserRepository repository;

        public UserController(AppDbContext context)
        {
            dbContext = context;
            repository = new UserRepository(dbContext);
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> List()
        {
            return Json(repository.ListAll());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<User> GetById([FromRoute] int id)
        {
            var dbUser = Json(repository.GetById(id));
            if (dbUser == null) return NotFound();
            return dbUser;
        }

        [HttpPost]
        public ActionResult Add([FromBody] User user)
        {
            repository.Add(user);
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
