using System;
using Model;
using TicketMicroService.Services;
using TicketMicroService.Util;
using Xunit;

namespace ProjTicketTestXUnit
{
    public class TicketTest
    {
        public class ProjMongoDBApiSettings : ITicketDatabase
        {
            public string TicketCollectionName { get; set; } = "Ticket";
            public string ConnectionString { get; set; } = "mongodb://localhost:27017";
            public string DatabaseName { get; set; } = "db_Ticket";
        }

        public TicketService InitializeDataBase()
        {
            var settings = new ProjMongoDBApiSettings();
            TicketService userService = new(settings);
            return userService;
        }

        [Fact]
        public void GetAll()
        {
            var ticketService = InitializeDataBase();
            ticketService.Get();

        }

        [Fact]
        public void GetId()
        {
            var ticketService = InitializeDataBase();
            var seachTicket = ticketService.Get("624e14defb4b2181ed9551d4");
            Assert.Equal("624e14defb4b2181ed9551d4", seachTicket.Id);
        }

        [Fact]
        public void Create()
        {
            var ticketService = InitializeDataBase();

            Ticket newTicket = new Ticket()
            {
                Id = "6255c6ef4d7da144fc170ad4",
                //DateRegister = 21/02/1900,
                Amount = 1600,
                Promotion = 10
            };

            ticketService.Create(newTicket);
            var seachTicket = ticketService.Get("6255c6ef4d7da144fc170ad4");
            Assert.Equal("6255c6ef4d7da144fc170ad4", seachTicket.Id);
        }

        [Fact]
        public void Update()
        {
            var ticketService = InitializeDataBase();

            Ticket updateTicket = new Ticket()
            {
                Id = "6251d4203041ac55e98b53e8",
                //DateRegister = 21/02/1900,
                Amount = 1600,
                Promotion = 25
            };

            ticketService.Update("6251d4203041ac55e98b53e8", updateTicket);
            var seachUser = ticketService.Get("6251d4203041ac55e98b53e8");
            Assert.Equal("6251d4203041ac55e98b53e8", seachUser.Id);
        }

        [Fact]
        public void Delete()
        {
            var ticketService = InitializeDataBase();

            ticketService.Remove("624dd220f9710d1433e231db");
            var seachTicket = ticketService.Get("624dd220f9710d1433e231db");
            Assert.Null(seachTicket);
        }
    }
}
