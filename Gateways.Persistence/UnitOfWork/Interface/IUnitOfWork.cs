using System;

namespace Gateways.Persistence.UnitOfWork.Interface
{
    public interface IUnitOfWork: IDisposable
    {
        int SaveChanges();
    }
}