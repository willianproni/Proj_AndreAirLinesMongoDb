using System.Collections.Generic;
using System.Threading.Tasks;
using AirportMicroServices.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

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

        [HttpGet("iata/{iata}", Name = "GetFlighty")]
        public ActionResult<Airport> GetAirportIata(string iata)
        {
            var airport = _airportService.GetIata(iata);

            if (airport == null)
                return NotFound("Aiport no Exist");

            return airport;
        }

[HttpPost]
        public async Task<ActionResult<Airport>> Create(Airport newAirport)
        {
            var addressAirport = await ServiceSeachViaCep.ServiceSeachCepInApiViaCep(newAirport.Address.Cep);
            newAirport.Address.City = addressAirport.City;
            newAirport.Address.Street = addressAirport.Street;
            newAirport.Address.State = addressAirport.State;
            newAirport.Address.District = addressAirport.District;

            if (_airportService.VerifyCodeIATA(newAirport.CodeIATA) != null)
                return Conflict("airport already registered\n\ttry again");

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
