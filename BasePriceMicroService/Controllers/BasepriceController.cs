using System.Collections.Generic;
using System.Threading.Tasks;
using BasePriceMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace BasePriceMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasepriceController : ControllerBase
    {
        private readonly BasepriceService _basepriceService;

        public BasepriceController(BasepriceService basepriceService)
        {
            _basepriceService = basepriceService;
        }

        [HttpGet]
        public ActionResult<List<BasePrice>> Get() =>
            _basepriceService.Get();

        [HttpGet("{id:length(24)}", Name = "GetBasePrice")]
        public ActionResult<BasePrice> Get(string id)
        {
            var baseprice = _basepriceService.Get(id);

            if (baseprice == null)
                return NotFound();

            return baseprice;
        }

        [HttpPost]
        public async Task<ActionResult<BasePrice>> Create(BasePrice newBaseprice)
        {
            if(!await ServiceSeachAirportExisting.CheckAirportService())
                return StatusCode(503, "Service Airport unavailable");

            var originAirport = await ServiceSeachAirportExisting.SeachAiportInApi(newBaseprice.Origin.CodeIATA);
            var destinyAirport = await ServiceSeachAirportExisting.SeachAiportInApi(newBaseprice.Destiny.CodeIATA);

            newBaseprice.Origin = originAirport;

            newBaseprice.Destiny = destinyAirport;


            _basepriceService.Create(newBaseprice);

            return CreatedAtRoute("GetBasePrice", new { id = newBaseprice.Id.ToString() }, newBaseprice);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, BasePrice upBaseprice)
        {
            var baseprice = _basepriceService.Get(id);

            if (baseprice == null)
                return NotFound();

            _basepriceService.Update(id, upBaseprice);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var baseprice = _basepriceService.Get(id);

            if (baseprice == null)
                return NotFound();

            _basepriceService.Remove(baseprice.Id);

            return NoContent();
        }
    }
}
