using System.Collections.Generic;
using AirportMicroServices.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

namespace AirportMicroServices.Controllers
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

        [HttpGet("{id:length(24)}", Name = "GetAirport")]
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
            return CreatedAtRoute("GetAirport", new { id = newAirport.Id.ToString() }, newAirport);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Airport upAirport)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound();

            _airportService.Uptade(id, upAirport);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound();

            _airportService.Remove(airport.Id);

            return NoContent();
        }

    }
}
