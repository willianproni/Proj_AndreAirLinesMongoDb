using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCWillFly.Models;
using Model;

namespace MVCWillFly.Data
{
    public class MVCWillFlyContext : DbContext
    {
        public MVCWillFlyContext (DbContextOptions<MVCWillFlyContext> options)
            : base(options)
        {
        }

        public DbSet<MVCWillFly.Models.Airport> Airport { get; set; }

        public DbSet<Model.Airport> Airport_1 { get; set; }

        public DbSet<Model.Aircraft> Aircraft { get; set; }
    }
}
