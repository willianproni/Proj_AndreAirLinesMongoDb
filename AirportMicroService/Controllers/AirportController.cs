using System.Collections.Generic;
using AirportMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AirportMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService;

        public AirportController(AirportService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public ActionResult<List<Airport>> Get() =>
            _airportService.Get();

        [HttpGet]
        public ActionResult<Airport> Get(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound();

            return airport;
        }

        [HttpPost]
        public ActionResult<Airport> Create(Airport newAirport)
        {
            _airportService.Create(newAirport);

            return CreatedAtRoute("GetPassenger", new { id = newAirport.Id.ToString() }, newAirport);
        }

        [HttpPut]
        public IActionResult Update(string id, Airport upAirport)
        {
            if (id == null)
                return NotFound();

            _airportService.Update(id, upAirport);

            return NoContent();
        }

        [HttpDelete]
        public IActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);

            if (id == null)
                return NotFound();

            _airportService.Remove(id);

            return NoContent();
        }
    }
}
