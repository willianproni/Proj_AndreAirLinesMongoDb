using AirportDataDarpperMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model.DataModel;
using System.Collections.Generic;

namespace AirportDataDarpperMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AirportDapperController : ControllerBase
    {
        private readonly AirportDataService _airportService = new AirportDataService();

        public AirportDapperController(AirportDataService airportService)
        {
            _airportService = airportService;
        }

        [HttpGet]
        public ActionResult<List<AirportData>> Get() =>
            _airportService.Get();

        [HttpGet]
        public ActionResult<AirportData> Get(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound("Airport No Exist, try again");

            return NoContent();
        }

        [HttpPost]
        public ActionResult<AirportData> Create(AirportData airport)
        {
            _airportService.Add(airport);
            return airport;
        }
        [HttpPut]
        public IActionResult Update(string id, AirportData upAirport)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound("Airport No Exist, try again");

            _airportService.Update(upAirport);
            return NoContent();
        }

        [HttpDelete]
        public ActionResult Delete(string id)
        {
            var airport = _airportService.Get(id);

            if (airport == null)
                return NotFound("Airport No Exist, try again");

            _airportService.Remove(id);
            return NoContent();
        }
            
    }
}
