using System.Collections.Generic;
using Model;
using MongoDB.Driver;
using TicketMicroService.Util;

namespace TicketMicroService.Services
{
    public class TicketService
    {
        private readonly IMongoCollection<Ticket> _ticket;

        public TicketService(ITicketDatabase settings)
        {
            var ticket = new MongoClient(settings.ConnectionString);
            var database = ticket.GetDatabase(settings.DatabaseName);
            _ticket = database.GetCollection<Ticket>(settings.TicketCollectionName);
        }

        public List<Ticket> Get() =>
            _ticket.Find(ticket => true).ToList();

        public Ticket Get(string id) =>
            _ticket.Find<Ticket>(ticket => ticket.Id == id).FirstOrDefault();

        public Ticket Create(Ticket newTicket)
        {
            _ticket.InsertOne(newTicket);
            return newTicket;
        }

        public void Update(string id, Ticket upTicket) =>
            _ticket.ReplaceOne(ticket => ticket.Id == id, upTicket);

        public void Remove(string id) =>
            _ticket.DeleteOne(ticket => ticket.Id == id);
    }
}
