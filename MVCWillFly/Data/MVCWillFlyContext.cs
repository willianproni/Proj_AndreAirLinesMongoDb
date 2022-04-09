using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MVCWillFly.Models;

namespace MVCWillFly.Data
{
    public class MVCWillFlyContext : DbContext
    {
        public MVCWillFlyContext (DbContextOptions<MVCWillFlyContext> options)
            : base(options)
        {
        }

        public DbSet<MVCWillFly.Models.Airport> Airport { get; set; }
    }
}
