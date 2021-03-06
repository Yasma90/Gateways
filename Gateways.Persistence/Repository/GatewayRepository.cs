using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gateways.Domain;
using Gateways.Persistence;
using Gateways.Persistence.Repository.Interface;

namespace Gateways.Persistence.Repository
{
    public class GatewayRepository : GenericRepository<Gateway>, IGatewayRepository
    {
        public GatewayRepository(GatewaysDbContext context) : base(context)
        {
        }
    }
}
