using System.Collections.Generic;
using FunctionMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace FunctionMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FunctionController : ControllerBase
    {
        private readonly FunctionService _functionService;

        public FunctionController(FunctionService functionService)
        {
            _functionService = functionService;
        }

        [HttpGet]
        public ActionResult<List<Function>> Get() =>
            _functionService.Get();

        [HttpGet("{id}")]
        public ActionResult<Function> Get(string id)
        {
            var returnSeachFunction = _functionService.Get(id);

            if (returnSeachFunction == null)
                return BadRequest("Function not Exist");

            return returnSeachFunction;
        }

        [HttpPost]
        public ActionResult<Function> Create(Function newFunction)
        {
            if (!string.IsNullOrEmpty(newFunction.Id))
            {
                var verifyExistFunction = _functionService.Get(newFunction.Id);

                if (verifyExistFunction != null)
                    return BadRequest("Function Id Exist, try New Id");

                _functionService.Create(newFunction);

                return CreatedAtRoute("GetFuction", new { id = newFunction.Id }, newFunction);
            }

            return BadRequest("Id cannot be null, try again");
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Function upFunction)
        {
            var verifyExistFunction = _functionService.Get(id);

            if (verifyExistFunction == null)
                return BadRequest("Funciton not exist");

            _functionService.Update(id, upFunction);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var verifyExistFunction = _functionService.Get(id);

            if (verifyExistFunction == null)
                return BadRequest("Funciton not exist");

            _functionService.Delete(id);

            return NoContent();
        }

    }
}
