using Gateways.Domaine;
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
            var dbName = $"GayewayssDb_{DateTime.Now.ToFileTimeUtc()}";
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
            var repository = await CreateRepositoryAsync();

            // Act
            await repository.AddAsync(new Gateway()
            {
                Name = "Some Gateway",
                PeripheralDevices = new List<PeripheralDevice>()
                {
                    new PeripheralDevice {
                        Vendor = $"Vendor_90",
                        Created = DateTime.Now,
                        Status = (DeviceStatus)1
                    }
                }
            });
            await _context.SaveChangesAsync();
            // Assert
            var gateways = await repository.GetAllAsync();
            Assert.AreEqual(4, gateways.Count);
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
                    PeripheralDevices = new List<PeripheralDevice>()
                    {
                    new PeripheralDevice
                    {
                        Vendor = $"Gateway_Vendor_1{index}",
                        Status = (DeviceStatus)0,
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
