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

        public DbSet<County> Counties { get; set; }
        public DbSet<Demographic> Demographics { get; set; }
        public DbSet<Population> Populations { get; set; }

    }
}