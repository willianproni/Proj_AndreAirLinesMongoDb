using System.Collections.Generic;
using FlightMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using System;
using Services;
using System.Threading.Tasks;
using System.Net.Http;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

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
        [AllowAnonymous]
        public ActionResult<List<Flight>> Get() =>
            _flightService.Get();

        [HttpGet("{id:length(24)}", Name = "GetFlight")]
        [Authorize]
        public ActionResult<Flight> get(string id)
        {
            var flight = _flightService.Get(id);

            if (flight == null)
                return NotFound();

            return flight;
        }

        [HttpPost]
        [Authorize(Roles = "Master, User")]
        public async Task<ActionResult<Flight>> Create(Flight newFlight)
        {
            Airport aiportOrigin, aiportDestiny;
            Aircraft aircraftApi;
            User permissionUser;

            try
            {
                permissionUser = await ServiceSeachApiExisting.SeachUserInApiByLoginUser(newFlight.LoginUser);

                if (permissionUser.Function.Id != "1" && permissionUser.Function.Id != "2")
                    return BadRequest("Access blocked, need manager/user permission");
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service User unavailable, start Api");
            }

            try
            {
                aiportOrigin = await ServiceSeachApiExisting.SeachAiportInApi(newFlight.Origin.CodeIATA);
                aiportDestiny = await ServiceSeachApiExisting.SeachAiportInApi(newFlight.Destiny.CodeIATA);
                aircraftApi = await ServiceSeachApiExisting.SeachAircraftNameInApi(newFlight.Aircraft.Name);

                newFlight.Origin = aiportOrigin;
                newFlight.Destiny = aiportDestiny;
                newFlight.Aircraft = aircraftApi;

                var newFlightJson = JsonConvert.SerializeObject(newFlight);
                PostLogApi.PostLogInApi(new Log(newFlight.LoginUser, null, newFlightJson, "Post"));

                _flightService.Create(newFlight);

                return CreatedAtRoute("GetFlight", new { id = newFlight.Id.ToString() }, newFlight);
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service Airport or Arcraft unavailable, start Api");
            }
        }

        [HttpPut("{id:length(24)}")]
        [Authorize(Roles = "Master, User")]
        public async Task<IActionResult> Update(string id, Flight upFlight)
        {
            User permissionUser;

            try
            {
                permissionUser = await ServiceSeachApiExisting.SeachUserInApiByLoginUser(upFlight.LoginUser);

                if (permissionUser.Function.Id != "1" && permissionUser.Function.Id != "2")
                    return BadRequest("Access blocked, need manager/user permission");
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service User unavailable, start Api");
            }

            var seachFlight = _flightService.Get(id);

            if (seachFlight == null)
                return NotFound();

            _flightService.Update(id, upFlight);

            var updateFlight = JsonConvert.SerializeObject(upFlight);
            var oldFlight = JsonConvert.SerializeObject(seachFlight);
            PostLogApi.PostLogInApi(new Log(upFlight.LoginUser, oldFlight, updateFlight, "Update"));

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        [Authorize(Roles = "Master, User")]
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
