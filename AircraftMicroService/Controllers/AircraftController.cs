using System.Collections.Generic;
using AircraftMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using UserMicroServices.Services;
using Services;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using ProjRabbitMQLogs.Service;

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
        //[AllowAnonymous]
        public ActionResult<List<Aircraft>> Get() =>
            _aircraftService.Get();

        [HttpGet("{nameAircraft}", Name = "GetAircraft")]
        //[Authorize]
        public ActionResult<Aircraft> GetArcraftName(string nameAircraft)
        {
            var SeachArcraft = _aircraftService.GetNameAircraft(nameAircraft);

            if (SeachArcraft == null)
                return NotFound();

            return SeachArcraft;
        }

        [HttpPost]
        //[Authorize(Roles = "Master")]
        public async Task<ActionResult<Aircraft>> Create(Aircraft newAircraft)
        {
            try
            {
                if (_aircraftService.VerifyAircraftExist(newAircraft.Name))
                    return Conflict("Aicraft already Registered, Try again");

                _aircraftService.Create(newAircraft);

                var aircraftJson = JsonConvert.SerializeObject(newAircraft);
                await SenderMongoDBservice.Add(new Log(newAircraft.LoginUser, null, aircraftJson, "Post"));

                return CreatedAtRoute("GetAircraft", new { Name = newAircraft.Name.ToString() }, newAircraft);
            }
            catch (Exception e)
            {
                return BadRequest("Exception " + e.Message);
            }

        }

        [HttpPut("{nameAircraft}")]
        //[Authorize(Roles = "Master")]
        public async IActionResult Update(string nameAircraft, Aircraft upAircraft)
        {
            var SeachArcraft = _aircraftService.GetNameAircraft(nameAircraft);

            if (SeachArcraft == null)
                return BadRequest("Aircraft does not exist in the database, check the data and try again");

            _aircraftService.Update(nameAircraft, upAircraft);

            var updateAircraftJson = JsonConvert.SerializeObject(upAircraft);
            var oldAicraft = JsonConvert.SerializeObject(SeachArcraft);
            await SenderMongoDBservice.Add(new Log(upAircraft.LoginUser, oldAicraft, updateAircraftJson, "Update"));

            return NoContent();
        }

        [HttpDelete("{nameAircraft}")]
        //[Authorize(Roles = "Master")]
        public IActionResult Delete(string nameAircraft)
        {
            var SeachArcraft = _aircraftService.GetNameAircraft(nameAircraft);

            if (SeachArcraft == null)
                return BadRequest("Aircraft does not exist in the database, check the data and try again");

            _aircraftService.Remove(SeachArcraft.Name);

            return NoContent();
        }
    }
}
