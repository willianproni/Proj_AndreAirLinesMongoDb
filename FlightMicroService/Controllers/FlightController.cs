﻿using System.Collections.Generic;
using FlightMicroService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;

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
        public ActionResult<Flight> Create(Flight newFlight)
        {

            _flightService.Create(newFlight);

            return CreatedAtRoute("GetFlight", new { id = newFlight.Id.ToString() }, newFlight);
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
                return NotFound();

            _flightService.Remove(flight.Id);

            return NoContent();

        }

    }
}