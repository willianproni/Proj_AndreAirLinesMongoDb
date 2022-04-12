using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using BasePriceMicroService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
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
        [AllowAnonymous]
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

        [HttpGet("AirportOriginAndDestiny")]
        [Authorize]
        public ActionResult<BasePrice> GetAirport(string codIataOrigin, string codIataDestiny)
        {
            var airport = _basepriceService.GetAirport(codIataOrigin, codIataDestiny);

            if (airport == null)
                return BadRequest("Airport Not found");

            return airport;
        }

        [HttpPost]
        [Authorize(Roles = "Master")]
        public async Task<ActionResult<BasePrice>> Create(BasePrice newBaseprice)
        {
            Airport originAirport, destinyAirport;

            try
            {
                originAirport = await ServiceSeachApiExisting.SeachAiportInApi(newBaseprice.Origin.CodeIATA);
            }
            catch (HttpRequestException)
            {

                return StatusCode(400, "Airport Origin Not exist in database, verify try again");
            }

            try
            {
                destinyAirport = await ServiceSeachApiExisting.SeachAiportInApi(newBaseprice.Destiny.CodeIATA);
            }
            catch (System.Exception)
            {

                return StatusCode(400, "Airport Destiny Not exist in database, verify try again");

            }


            newBaseprice.Origin = originAirport;

            newBaseprice.Destiny = destinyAirport;

            _basepriceService.Create(newBaseprice);

            var newBasePriceJson = JsonConvert.SerializeObject(newBaseprice);
            PostLogApi.PostLogInApi(new Log(newBaseprice.LoginUser, null, newBasePriceJson, "Post"));

            return CreatedAtRoute("GetBasePrice", new { id = newBaseprice.Id.ToString() }, newBaseprice);
        }

        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = "Master")]
        public IActionResult Update(string id, BasePrice upBaseprice)
        {
            var seachBasePrice = _basepriceService.Get(id);

            if (seachBasePrice == null)
                return NotFound();

            _basepriceService.Update(id, upBaseprice);

            var updateBasePriceJson = JsonConvert.SerializeObject(upBaseprice);
            var oldBasePriceJson = JsonConvert.SerializeObject(seachBasePrice);
            PostLogApi.PostLogInApi(new Log(upBaseprice.LoginUser, oldBasePriceJson, updateBasePriceJson, "Update"));

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "Master")]
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
