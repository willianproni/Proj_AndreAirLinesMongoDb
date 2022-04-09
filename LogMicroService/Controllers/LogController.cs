using System.Collections.Generic;
using LogMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace LogMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly LogService _logService;

        public LogController(LogService logService)
        {
            _logService = logService;
        }

        [HttpGet]
        public ActionResult<List<Log>> Get() =>
            _logService.Get();

        [HttpGet("{id}", Name = "GetLog")]
        public ActionResult<Log> Get(string id)
        {
           var seachLog =  _logService.Get(id);

            if (seachLog == null)
                return BadRequest("Log Not Exist");

            return seachLog;
        }

        [HttpPost]
        public ActionResult<Log> Create(Log newLog)
        {
            _logService.Create(newLog);

            return CreatedAtRoute("GetLog", new { id = newLog.Id.ToString() }, newLog);
        }

        [HttpPut("{id}")]
        public IActionResult Update(string id, Log updateLog)
        {
            var verifyLog = _logService.Get(id);

            if (verifyLog == null)
                return BadRequest("Log not exist, try again");

            _logService.Update(id, updateLog);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(string id)
        {
            var verifyLog = _logService.Get(id);

            if (verifyLog == null)
                return BadRequest("log not exist, try again");

            _logService.Remove(id);

            return NoContent();
        }
            
    }
}
