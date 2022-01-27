using Gateways.Domain;
using Gateways.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.Persistence
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public DbInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Initialize()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<GatewaysDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }

        public void SeedData()
        {
            using (var serviceScope = _scopeFactory.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<GatewaysDbContext>())
                {
                    if (!context.Gateways.Any())
                    {
                        var gateway = new Gateway
                        {
                            IPAddress = "127.0.0.1",
                            Name = "Xiaomi",
                            SerialNumber = Guid.NewGuid().ToString()
                        };
                        var devices = new List<PeripheralDevice>()
                        {
                            new PeripheralDevice
                            {
                                Gateway = gateway,
                                Vendor = "Xiaomi ABO",
                                GatewayId = gateway.Id,
                            },
                            new PeripheralDevice
                            {
                                Gateway = gateway,
                                Vendor = "Xiaomi XXX",
                                GatewayId = gateway.Id,
                                Status = (DeviceStatus)1
                            }
                        };
                        context.Gateways.Add(gateway);
                        context.PeripheralDevices.AddRange(devices);
                    }

                    context.SaveChanges();
                }
            }
        }
    }
}
