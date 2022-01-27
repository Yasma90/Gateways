using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gateways.Domain;
using Gateways.Persistence;
using Gateways.Persistence.Repository.Interface;

namespace Gateways.Persistence.Repository
{
    public class PeripheralDeviceRepository : GenericRepository<PeripheralDevice>, IPeripheralDeviceRepository
    {
        public PeripheralDeviceRepository(GatewaysDbContext context) : base(context)
        {
        }
    }
}
