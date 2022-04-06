using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using PassengerMicroService.Services;
using Services;
using System;
using System.Threading.Tasks;

namespace PassengerMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PassengerController : ControllerBase
    {
        private readonly PassengerService _passengerService;

        public PassengerController(PassengerService passengerService)
        {
            _passengerService = passengerService;
        }

        [HttpGet]
        public ActionResult<List<Passenger>> Get() =>
            _passengerService.Get();

        [HttpGet("{id:length(24)}", Name = "GetPassenger")]
        public ActionResult<Passenger> Get(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
                return NotFound();

            return passenger;
        }

        [HttpGet("cpf/{cpf}", Name = "GetPassengerCpf")]
        public ActionResult<Passenger> GetCpf(string cpf)
        {
            var passenger = _passengerService.VerifyCpfPassenger(cpf);

            if (passenger == null)
                return NotFound("unregistered passenger, try again");

            return passenger;
        }

        [HttpPost]
        public async Task<ActionResult<Passenger>> Create(Passenger newPassenger)
        {
            var address = await ServiceSeachViaCep.ServiceSeachCepInApiViaCep(newPassenger.Address.Cep);
            newPassenger.Address.City = address.City;
            newPassenger.Address.Street = address.Street;
            newPassenger.Address.State = address.State;
            newPassenger.Address.District = address.District;
            newPassenger.Address.Complement = address.Complement;
            try
            {
                if (ValidateCpfPasseger.ValidCpfPassenger(newPassenger.Cpf) == true)
                    return Conflict("Cpf invalid, try again");

                if (_passengerService.VerifyPassengerExist(newPassenger.Cpf))
                    return BadRequest("Passenger Exist, try again");

                _passengerService.Create(newPassenger);
            }
            catch (Exception e)
            {
                return BadRequest("Exception " + e.Message);
            }


            return CreatedAtRoute("GetPassenger", new { id = newPassenger.Id.ToString() }, newPassenger);

        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Passenger upAassenger)
        {
            if (upAassenger == null)
                return NotFound();

            _passengerService.Update(id, upAassenger);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var passenger = _passengerService.Get(id);

            if (passenger == null)
                return NotFound();

            _passengerService.Remove(passenger.Id);

            return NoContent();
        }
    }
}
