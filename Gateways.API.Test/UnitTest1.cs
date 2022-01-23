using Gateways.Domaine;
using Gateways.Persistence;
using Gateways.Persistence.Repository;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;

namespace Gateways.API.Test
{
    public class Tests
    {
        [Test]
        public void GetAllTest()
        {
            var options = new DbContextOptionsBuilder<GatewaysDbContext>()
                .UseInMemoryDatabase(databaseName: "GatewaysTestDb")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new GatewaysDbContext(options))
            {
                var gateways = new List<Gateway>
                {
                    new Gateway
                    {
                        Name = "Huawei",
                        IPAddress="192.168.152.1",
                        SerialNumber = "fg45s-586ed-45ert-4523d"
                    },
                    new Gateway
                    {
                        Name = "Gigabit",
                        IPAddress="10.0.12.1",
                        SerialNumber = "fwerf3-e32d-e6rt7-y834t"
                    }
                };
                context.Gateways.AddRange(gateways);
                context.SaveChanges();
            }

            // Use a clean instance of the context to run the test
            using (var context = new GatewaysDbContext(options))
            {
                var gatewayRepository = new GatewayRepository(context);
                var gateways = gatewayRepository.GetAllAsync();

                Assert.AreEqual(3, gateways.Result.Count);
            }
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }
    }
}
