using System.Collections.Generic;
using ClasseMicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace ClasseMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClasseController : ControllerBase
    {
        private readonly ClasseService _classeService;

        public ClasseController(ClasseService classeService)
        {
            _classeService = classeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult<List<Classes>> Get() =>
            _classeService.Get();

        [HttpGet("{id}", Name = "GetClasse")]
        [Authorize]
        public ActionResult<Classes> Get(string id)
        {
            var classe = _classeService.Get(id);
             
            if (classe == null)
                return NotFound();

            return classe;
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public ActionResult<Classes> Create(Classes classe)
        {
            _classeService.Create(classe);

            return CreatedAtRoute("GetClasse", new { id = classe.Id.ToString() }, classe);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Master")]
        public IActionResult Update(string id, Classes newClasse)
        {
            var classe = _classeService.Get(id);

            if (classe == null)
                return NotFound();

            _classeService.Update(id, newClasse);

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Master")]
        public IActionResult Delete(string id)
        {
            var classe = _classeService.Get(id);

            if (classe == null)
                return NotFound("Absent ID, try again");

            _classeService.Remove(classe.Id);

            return NoContent();
        }
    }
}
