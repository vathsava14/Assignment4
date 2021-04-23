using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Assignment4.Models;

namespace Assignment4.DataAccess
{
    public class Assignment4DbContext : DbContext
    {
        public Assignment4DbContext(DbContextOptions<Assignment4DbContext> options) : base(options)
        {
        }

        public DbSet<Sector> Sector { get; set; }
        public DbSet<EnergySource> EnergySource { get; set; }
        public DbSet<AnnualEnergyConsumption> AnnualEnergyConsumption { get; set; }

    }
}