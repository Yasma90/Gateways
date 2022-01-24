using Moq;
using System;
using Autofac;
using NUnit.Framework;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Gateways.Domaine;
using Gateways.Persistence;
using Gateways.Persistence.Repository;
using Gateways.Persistence.Repository.Interface;
using Gateways.Persistence.UnitOfWork.Interface;

namespace Gateways.API.Test
{
    public class Tests
    {
        private DbContextOptions<GatewaysDbContext> _options;

        private Mock<IUnitOfWork> _uow;
        private Mock<GatewaysDbContext> _dbContext;
        private GatewayRepository _gatewayRepository;
        private List<Gateway> _gateways;
        private List<PeripheralDevice> _devices;
        private Gateway _gateway;
        private Random _random;

        [SetUp]
        public void Setup()
        {
            //Create in Memory Database
            InitDbTest();
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

            //_random = new Random();
            ////Setup DbContext and DbSet mock
            //var dbContextMock = new Mock<GatewaysDbContext>();
            //var dbSetMock = new Mock<DbSet<Gateway>>();

            ////var tsk = Task.FromResult(new Gateway());
            //dbSetMock.Setup(s => s.Find(It.IsAny<int>())).Returns(new Gateway() { Id = 1 });
            //dbContextMock.Setup(s => s.Set<Gateway>()).Returns(dbSetMock.Object);

            //Execute method of UT (GatewayRepository)

            //InitDbTest();
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

        [Test]
        public async Task GetAll()
        {
            
        }
        public async Task AddSave()
        {
            var model = new Gateway
            {
                Id = _random.Next(4,10),
                Name = $"Gateway Test{_random.Next()}",
                SerialNumber = $"{_random.Next()}XN-ST{_random.Next()}-EP{_random.Next()}EDT",
                IPAddress = "127.0.0.1"
            };

            var result = await _gatewayRepository.AddAsync(model);

            //result.Name.Should().Be(model.Name);
            //result.IPAddress.Should().Be(model.IPAddress);
            //result.SerialNumber.Should().Be(model.SerialNumber);
            //result.Id.Should().Be(model.Id);
           
            //_uow.Verify(x => x.GatewayRepository.AddAsync(result));
            //_uow.Verify(x => x.SaveChangesAsync());
        }
        
        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

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
    }
}
