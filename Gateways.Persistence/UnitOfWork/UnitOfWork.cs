using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Gateways.Persistence;
using Gateways.Persistence.UnitOfWork.Interface;
using Gateways.Persistence.Repository;
using Gateways.Persistence.Repository.Interface;

namespace Gateways.Persistence.UnitOfWork
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly GatewaysDbContext _context;
        public IGatewayRepository GatewayRepository { get; set; } 
        public IPeripheralDeviceRepository PeripheralDeviceRepository { get; set; }
        private bool disposed;

        public UnitOfWork(GatewaysDbContext context, IGatewayRepository gatewayRepository, IPeripheralDeviceRepository deviceRepository)
        {
            _context = context;
            GatewayRepository = gatewayRepository;
            PeripheralDeviceRepository = deviceRepository;
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
