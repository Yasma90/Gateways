using Gateways.Domaine;
using Gateways.Persistence;
using Gateways.Persistence.Repository;
using Gateways.Persistence.UnitOfWork.Interface;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gateways.API.Test
{
    public class GatewaysControllerTest
    {
        private DbContextOptions<GatewaysDbContext> _options;

        [SetUp]
        public void Setup()
        {
            //Create in Memory Database
            InitDbTest();
        }

        [Test]
        public async Task GetGatewaysAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            var authorList = await repository.GetAllAsync();

            // Assert
            Assert.AreEqual(3, authorList.Count);
        }

        [Test]
        public void GetGatewayByIdTest()
        {
            // Use a clean instance of the context to run the test
            using (var context = new GatewaysDbContext(_options))
            {
                var gatewayRepository = new GatewayRepository(context);
                var gateway = gatewayRepository.GetbyIdAsync(1);

                Assert.AreEqual("Huawei", gateway.Result.Name);
                Assert.AreEqual("192.168.152.1", gateway.Result.IPAddress);
                Assert.AreEqual("fg45s-586ed-45ert-4523d", gateway.Result.SerialNumber);
            }
        }

        [Test]
        public void GetByIdAsync_Returns_Gateway()
        {
            // Use a clean instance of the context to run the test
            using (var context = new GatewaysDbContext(_options))
            {
                var gatewayRepository = new GatewayRepository(context);
                var gateway = gatewayRepository.GetbyIdAsync(1).Result;

                //Assert
                Assert.NotNull(gateway);
                Assert.IsAssignableFrom<Gateway>(gateway);
            }
        }

        public async Task GetAuthorByIdAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            var author = await repository.GetbyIdAsync(1);

            // Assert
            Assert.NotNull(author);
            Assert.AreEqual("Author_1", author.Name);
            Assert.AreEqual(2, author.PeripheralDevices.Count);
        }

        [Test]
        public async Task CreateAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            await repository.AddAsync(new Gateway()
            {
                Name = "Some Author",
                PeripheralDevices = new List<PeripheralDevice>()
            {
                new PeripheralDevice { Vendor = $"Vendor_90",
                        Status = (DeviceStatus)1,
                        Created = DateTime.Now }
            }
            });

            // Assert
            var gateways = await repository.GetAllAsync();
            Assert.AreEqual(4, gateways.Count);
        }

        [Test]
        public async Task DeleteAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            await repository.DeleteAsync(3);

            // Assert
            var gateways = await repository.GetAllAsync();
            Assert.AreEqual(2, gateways.Count);
        }


        #region Help Methods

        private void InitDbTest()
        {
            _options = new DbContextOptionsBuilder<GatewaysDbContext>()
                .UseInMemoryDatabase(databaseName: "GatewaysTestDb")
                .Options;

            // Insert seed data into the database using one instance of the context
            using (var context = new GatewaysDbContext(_options))
            {
                var gateways = new List<Gateway>
                {
                    new Gateway
                    {
                        Id= 1,
                        Name = "Huawei",
                        IPAddress="192.168.152.1",
                        SerialNumber = "fg45s-586ed-45ert-4523d"
                    },
                    new Gateway
                    {
                        Id=2,
                        Name = "Gigabit",
                        IPAddress="10.0.12.1",
                        SerialNumber = "fwerf3-e32d-e6rt7-y834t"
                    },
                    new Gateway
                    {
                        Id=3,
                        Name = "Xiaomi",
                        IPAddress="12.114.22.78",
                        SerialNumber = "f2edrgt-67h2d-e5st7-45rtt"
                    }
                };
                context.Gateways.AddRange(gateways);
                context.SaveChanges();
            }

        }

        private async Task<GatewayRepository> CreateRepositoryAsync()
        {
            var context = new GatewaysDbContext(_options);
            await PopulateDataAsync(context);
            return new GatewayRepository(context);
        }

        private async Task PopulateDataAsync(GatewaysDbContext context)
        {
            int index = 1;

            while (index <= 3)
            {
                var author = new Gateway()
                {
                    Name = $"Gateway_{index}",
                    PeripheralDevices = new List<PeripheralDevice>()
                {
                    new PeripheralDevice
                    {
                        Vendor = $"Vendor_1{index+4}",
                        Status = (DeviceStatus)0,
                        Created = DateTime.Now
                    },
                    new PeripheralDevice
                    {
                        Vendor = $"Vendor_{index+3}2",
                        Status = (DeviceStatus)1,
                        Created = DateTime.Now
                    },
                }
                };

                index++;
                await context.Gateways.AddAsync(author);
            }

            await context.SaveChangesAsync();
        }

        #endregion
    }


}
