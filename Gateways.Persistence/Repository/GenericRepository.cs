﻿using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Gateways.Persistence.Repository.Interface;
using Gateways.Persistence.DbContext;

namespace Gateways.Persistence.Repository
{
    public abstract class GenericRepository<TEntity /*,TContext*/> : IGenericRepository<TEntity>, IDisposable
       where TEntity : class
       //where TContext : Microsoft.EntityFrameworkCore.DbContext
    {

        private readonly GatewaysDbContext _context; //TContext
        private readonly DbSet<TEntity> _dbSet;
        private bool disposed;

        public GenericRepository(GatewaysDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            return entities;
        }

        public async Task<TEntity> DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity == null)
                return entity;

            _dbSet.Remove(entity);
            return entity;
        }

        public IEnumerable<TEntity> DeleteRange(IEnumerable<TEntity> entities)
        {
            _dbSet.RemoveRange(entities);
            return entities;
        }

        public async Task<TEntity> GetAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public TEntity Update(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public IEnumerable<TEntity> UpdateRange(IEnumerable<TEntity> entities)
        {
            _dbSet.UpdateRange(entities);
            return entities;
        }

        #region Dispose

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                    _context.Dispose();
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposed = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~GenericRepository()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        void IDisposable.Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
        #endregion

    }
}