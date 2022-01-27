using Gateways.Domain;
using Gateways.Persistence;
using Gateways.Persistence.Repository;
using Gateways.Persistence.Repository.Interface;
using Gateways.Persistence.UnitOfWork;
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
        DbContextOptions<GatewaysDbContext> _options;
        GatewaysDbContext _context;

        [SetUp]
        public void Setup()
        {
            //Create in Memory Database
            var dbName = $"GatewaysDb_{DateTime.Now.ToFileTimeUtc()}";
            _options = new DbContextOptionsBuilder<GatewaysDbContext>()
            .UseInMemoryDatabase(databaseName: dbName)
            .Options;

            _context = new GatewaysDbContext(_options);
        }

        [Test]
        public async Task GetGatewaysAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            var gateways = await repository.GetAllAsync();

            // Assert
            Assert.AreEqual(3, gateways.Count);
        }

        [Test]
        public async Task GetGatewayByIdAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            var gateway = await repository.GetbyIdAsync(1);

            // Assert
            Assert.NotNull(gateway);
            Assert.AreEqual("Gateway_1", gateway.Name);
            Assert.AreEqual(2, gateway.PeripheralDevices.Count);
        }

        [Test]
        public async Task DeleteAsync_Success_Test()
        {
            var repository = await CreateRepositoryAsync();

            // Act
            await repository.DeleteAsync(2);
            await _context.SaveChangesAsync();
            // Assert
            var gateways = await repository.GetAllAsync();
            Assert.AreEqual(2, gateways.Count);
        }

        [Test]
        public async Task CreateAsync_Success_Test()
        {
            var gatRepository = await CreateRepositoryAsync();
            var devRepository = new PeripheralDeviceRepository(_context);

            // Act
            var gateway = new Gateway()
            {
                IPAddress = "localhost",
                Name = "Some Gateway",
                SerialNumber = Guid.NewGuid().ToString()
            };
            await gatRepository.AddAsync(gateway);

            var device = new PeripheralDevice()
            {
                Vendor = "Iphone",
                Status = 0,
                GatewayId = gateway.Id
            };
            await devRepository.AddAsync(device);
            await _context.SaveChangesAsync();

            // Assert
            var gateways = await gatRepository.GetAllAsync();
            var devices = await devRepository.GetAsync(d => d.GatewayId == gateway.Id);
            Assert.AreEqual(4, gateways.Count);
            Assert.AreEqual(1, devices.Count);
        }

        #region Help Methods

        private async Task<GatewayRepository> CreateRepositoryAsync()
        {
            await PopulateDataAsync();
            return new GatewayRepository(_context);
        }

        private async Task PopulateDataAsync()
        {
            int index = 1;

            while (index <= 3)
            {
                var gateway = new Gateway()
                {
                    IPAddress = $"127.0.0.{index}",
                    Name = $"Gateway_{index}",
                    SerialNumber = Guid.NewGuid().ToString(),
                    PeripheralDevices = new List<PeripheralDevice>()
                    {
                        new PeripheralDevice
                        {
                            Vendor = $"Gateway_Vendor_1{index}",
                            Status = index%2==0? 0: (DeviceStatus)1,
                            Created = DateTime.Now
                        },
                        new PeripheralDevice
                        {
                            Vendor = $"Gateway_Vendor_2{index}",
                            Status = (DeviceStatus)1,
                            Created = DateTime.Now
                        },
                    }
                };

                index++;
                await _context.Gateways.AddAsync(gateway);
            }

            await _context.SaveChangesAsync();
        }

        #endregion
    }


}
