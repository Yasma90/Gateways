using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gateways.Persistence.Repository.Interface
{
    public interface IGenericRepository<T> where T:class 
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(int Id);
        Task<T> AddAsync(T entity);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities);
        T Update(T entity);
        IEnumerable<T> UpdateRange(IEnumerable<T> entities);
        Task<T> DeleteAsync(int id);
        IEnumerable<T> DeleteRange(IEnumerable<T> entities);
    }
}
