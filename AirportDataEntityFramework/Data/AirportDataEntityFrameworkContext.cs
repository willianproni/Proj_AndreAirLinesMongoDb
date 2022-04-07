using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Model.DataModel;

namespace AirportDataEntityFramework.Data
{
    public class AirportDataEntityFrameworkContext : DbContext
    {
        public AirportDataEntityFrameworkContext (DbContextOptions<AirportDataEntityFrameworkContext> options)
            : base(options)
        {
        }

        public DbSet<Model.DataModel.AirportData> AirportData { get; set; }
    }
}
