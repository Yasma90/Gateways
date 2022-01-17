using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Gateways.Domine;
using Microsoft.EntityFrameworkCore.Migrations.Internal;

namespace Gateways.Persistence.DbContext
{
    public class GatewaysDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<PeripheralDevice> PeripheralDevices { get; set; }

        public GatewaysDbContext(DbContextOptions<GatewaysDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder){}

    }

}
