using BeersApi.Dto;
using BeersApi.Models.Abstractions;
using BeersApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BeersApi.Repositories.Abstractions
{
    public abstract class Repository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly DbSet<T> _dbSet;
        public Repository(DbSet<T> dbSet)
        {
            _dbSet = dbSet;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.Where(x => x.Status).ToListAsync();
        }

        public async Task<Pages<T>> GetAll(int? offset, int? limit)
        {
            IQueryable<T> query = _dbSet.Where(x => x.Status);
            List<T> elements = await query.OrderBy(x => x.Id).Skip(offset ?? 0).Take(limit ?? 10).ToListAsync();
            int totalItems = await query.CountAsync();
            return new Pages<T>()
            {
                Elements = elements,
                TotalItems = totalItems
            };
        }

        public Task<T?> GetById(Guid id)
        {
            return _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.Status);
        }
    }
}
