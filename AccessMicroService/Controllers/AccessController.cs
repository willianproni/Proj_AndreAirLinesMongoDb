using System.Collections.Generic;
using AccessMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AccessMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccessController : ControllerBase
    {
        private readonly AccessService _accessService;

        public AccessController(AccessService accessService)
        {
            _accessService = accessService;
        }

        [HttpGet]
        public ActionResult<List<Access>> Get() =>
            _accessService.Get();

        [HttpGet("{id}")]
        public ActionResult<Access> Get(string id)
        {
            var access = _accessService.Get(id);

            if (access == null)
                return Conflict("Not exist Access");

            return access;
        }

        [HttpPost]
        public ActionResult<Access> Create(Access newAccess)
        {
            if (!string.IsNullOrEmpty(newAccess.Id))
            {
                var access = _accessService.Get(newAccess.Id);

                if (access != null)
                    return Conflict("Access Id Exist");

                _accessService.Create(newAccess);

                return CreatedAtRoute("GetAccess", new { id = newAccess.Id }, newAccess);
            }

            return BadRequest("Id tem Superior a 0");

        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Access upAccess)
        {
            var access = _accessService.Get(id);

            if (access == null)
                return Conflict("Access Not Exist");

            _accessService.Update(id, upAccess);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var access = _accessService.Get(id);

            if (access == null)
                return Conflict("Access Not Exist");

            _accessService.Delete(id);

            return NoContent();
        }

    }
}
