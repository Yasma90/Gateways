using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Gateways.Domaine;

namespace Gateways.Persistence
{
    public class GatewaysDbContext : DbContext
    {
        public virtual DbSet<Gateway> Gateways { get; set; }
        public virtual DbSet<PeripheralDevice> PeripheralDevices { get; set; }

        public GatewaysDbContext(DbContextOptions<GatewaysDbContext> options)
           : base(options)
        {
        }

        //protected override void OnConfiguring(DbContextOptionsBuilder options)
        //    => options.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=GatewaysDb;Trusted_Connection=true;MultipleActiveResultSets=true;")
        //    .EnableSensitiveDataLogging(true);
          
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeripheralDevice>()
                .HasOne(g => g.Gateway)
                .WithMany(d => d.PeripheralDevices)
                .HasForeignKey(d => new { d.GatewayId })
                .HasPrincipalKey(g => new { g.Id })
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Gateway>(g =>
            {
                g.HasIndex(g => g.SerialNumber)
                .IsUnique();
            });


        }

    }

}

//// Set |DataDirectory| value : AttachDbFilename
//AppDomain.CurrentDomain.SetData("DataDirectory", "C:\myDB");