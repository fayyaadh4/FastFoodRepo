using FastFood.Domain.RepoInterfaces;
using FastFood.Repo.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FastFood.Infrastructure.Implementation
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DataContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(DataContext context)
        {
            _context = context;
            // connect to db context and set to type T to be used
            this._dbSet = context.Set<T>();
        }
        public async Task<bool> Add(T entity)
        {
            await _dbSet.AddAsync(entity);
            return true;
        }
        public async Task<bool> Exists(int id)
        {

            var exists = await _dbSet.FindAsync(id);
            return exists == null ? false : true;
        }

        public async Task<ICollection<T>> GetAll()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<bool> Remove(T entity)
        {
            _dbSet.Remove(entity);
            return true;
        }

        public async Task<bool> RemoveMany(ICollection<T> entities)
        {
            _dbSet.RemoveRange(entities);
            return true;
        }

        public async Task<bool> Update(T entity)
        {
            _dbSet.Update(entity);
            return true;
        }
    }
}
