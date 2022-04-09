using System.Collections.Generic;
using FlightMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using Services;
using System.Threading.Tasks;
using System.Net.Http;

namespace FlightMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FlightController : ControllerBase
    {
        private readonly FlightService _flightService;

        public FlightController(FlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet]
        public ActionResult<List<Flight>> Get() =>
            _flightService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFlight")]
        public ActionResult<Flight> get(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return NotFound();

            return flight;
        }

        [HttpPost]
        public async Task<ActionResult<Flight>> Create(Flight newFlight)
        {
            Airport aiportOrigin, aiportDestiny;
            Aircraft aircraftApi;

            try
            {
                aiportOrigin = await ServiceSeachApiExisting.SeachAiportInApi(newFlight.Origin.CodeIATA);
                aiportDestiny = await ServiceSeachApiExisting.SeachAiportInApi(newFlight.Destiny.CodeIATA);
                aircraftApi = await ServiceSeachApiExisting.SeachAircraftNameInApi(newFlight.Aircraft.Name);

                newFlight.Origin = aiportOrigin;
                newFlight.Destiny = aiportDestiny;
                newFlight.Aircraft = aircraftApi;

                _flightService.Create(newFlight);

                return CreatedAtRoute("GetFlight", new { id = newFlight.Id.ToString() }, newFlight);
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service Airport or Arcraft unavailable, start Api");
            }
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Flight upFlight)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return NotFound();

            _flightService.Update(id, upFlight);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return StatusCode(404, "Airport Not Exist");

            _flightService.Remove(flight.Id);

            return NoContent();

        }

    }
}
