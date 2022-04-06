using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using TicketMicroService.Services;

namespace TicketMicroService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly TicketService _ticketService;

        public TicketController(TicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult<List<Ticket>> Get() =>
            _ticketService.Get();

        [HttpGet("{id:length(24)}", Name = "GetTicket")]
        public ActionResult<Ticket> Get(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
                return NotFound();

            return ticket;
        }

        [HttpPost]
        public async Task<ActionResult<Ticket>> Create(Ticket newTicket)
        {
            var passenger = await ServiceSeachPassengerExisting.SeachPassengerInApi(newTicket.Passenger.Cpf);
            var flight = await ServiceSeachFlightExisting.SeachFlightApi(newTicket.Flight.Id);
            var classe = await ServiceSeachApiExisting.SeachClasseIdInApi(newTicket.Classes.Id);
            var basePrice = await ServiceSeachApiExisting.SeachBasepriceIdInApi(newTicket.BasePrice.Id);

            newTicket.Passenger = passenger;
            newTicket.Flight = flight;
            newTicket.Classes = classe;
            newTicket.BasePrice = basePrice;

            _ticketService.Create(newTicket);
            return CreatedAtRoute("GetTicket", new { id = newTicket.Id.ToString() }, newTicket);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Ticket upTicket)
        {
            var ticket = _ticketService.Get(id);

            if (upTicket == null)
                return NotFound();

            _ticketService.Update(id, upTicket);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var ticket = _ticketService.Get(id);

            if (ticket == null)
                return NotFound();

            _ticketService.Remove(ticket.Id);

            return NoContent();
        }
    }
}
