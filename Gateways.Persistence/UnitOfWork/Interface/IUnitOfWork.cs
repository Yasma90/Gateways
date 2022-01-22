using Gateways.Persistence.Repository.Interface;
using System;
using System.Threading.Tasks;

namespace Gateways.Persistence.UnitOfWork.Interface
{
    public interface IUnitOfWork : IDisposable
    {
        IGatewayRepository GatewayRepository { get; set; }
        IPeripheralDeviceRepository PeripheralDeviceRepository { get; set; }
        Task<int> SaveChangesAsync();
    }
}