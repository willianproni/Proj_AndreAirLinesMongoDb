using System.Collections.Generic;
using AircraftMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;

namespace AircraftMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AircraftController : ControllerBase
    {
        private readonly AircraftService _aircraftService;

        public AircraftController(AircraftService aircraftService)
        {
            _aircraftService = aircraftService;
        }

        [HttpGet]
        public ActionResult<List<Aircraft>> Get() =>
            _aircraftService.Get();

        [HttpGet("{id:length(24)}", Name = "GetAircraft")]
        public ActionResult<Aircraft> Get(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            return aircraft;
        }

        [HttpPost]
        public ActionResult<Aircraft> Create(Aircraft newAircraft)
        {
            try
            {
                if (_aircraftService.VerifyAircraftExist(newAircraft.Name))
                    return Conflict("Aicraft already Registered, Try again");

                _aircraftService.Create(newAircraft);
            }
            catch (Exception e)
            {
                return BadRequest("Exception " + e.Message);
            }
            
            return CreatedAtRoute("GetAircraft", new { id = newAircraft.Id.ToString() }, newAircraft);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Aircraft upAircraft)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            _aircraftService.Update(id, upAircraft);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var aircraft = _aircraftService.Get(id);

            if (aircraft == null)
            {
                return NotFound();
            }

            _aircraftService.Remove(aircraft.Id);

            return NoContent();
        }
    }
}
