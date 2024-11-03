
using Application.IRepository;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repository
{
    public class GenericRepo<T> : IGenericRepo<T> where T : class
    {
        protected DbSet<T> _dbSet;
        protected readonly ZodiacTinderContext context;
        public GenericRepo(ZodiacTinderContext context)
        {
            this.context = context;
            _dbSet = context.Set<T>();
        }
        public async Task AddAsync(T entity)
        {
            _ = await _dbSet.AddAsync(entity);
            _ = await context.SaveChangesAsync();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task Remove(T entity)
        {
            _ = _dbSet.Remove(entity);
            _ = await context.SaveChangesAsync();
        }

        public async Task Update(T entity)
        {
            _ = _dbSet.Update(entity);
            _ = await context.SaveChangesAsync();
        }

        public async Task<int> SaveChangeAsync()
        {
            return await context.SaveChangesAsync();
        }
    }
}
