using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.DataModel;

namespace EntityFrameworkAirport.Data
{
    public class EntityFrameworkAirportContext : DbContext
    {
        public EntityFrameworkAirportContext (DbContextOptions<EntityFrameworkAirportContext> options)
            : base(options)
        {
        }

        public DbSet<Model.DataModel.AirportData> AirportData { get; set; }
    }
}
