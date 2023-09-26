using BeersApi.Dto;
using BeersApi.Models.Abstractions;
using BeersApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BeersApi.Repositories.Abstractions
{
    public abstract class CrudRepository<T> : ICrudRepository<T> where T : BaseEntity
    {

        private readonly DbSet<T> _dbSet;
        private readonly Func<CancellationToken, Task<int>> _saveChangesAsync;
        private readonly Func<T, EntityEntry<T>> _entry;


        public CrudRepository(DbSet<T> dbSet, Func<CancellationToken, Task<int>> saveChangesAsync, Func<T, EntityEntry<T>> entry)
        {
            _dbSet = dbSet;
            _saveChangesAsync = saveChangesAsync;
            _entry = entry;
        }

        public async Task<bool> Delete(int id, CancellationToken cancellation = default)
        {
            T? entity = await _dbSet.Where(x => x.Id == id && x.Status).FirstOrDefaultAsync();
            if (entity == null) return false;
            entity.Status = false;
            await _saveChangesAsync(cancellation);
            return true;
        }

        public async Task<List<T>> GetAll()
        {
            return await _dbSet.Where(x => x.Status).ToListAsync();
        }

        public async Task<Pages<T>> GetAll(int? offset, int? limit)
        {
            IQueryable<T> query = _dbSet.Where(x => x.Status);
            List<T> result = await query.OrderBy(x => x.Id).Take(limit ?? 10).Skip(offset ?? 0).ToListAsync();
            int totalItems = await query.CountAsync();
            return new Pages<T>()
            {
                TotalItems = totalItems,
                Elements = result
            };
        }

        public async Task<T?> GetById(int id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id && x.Status);
        }

        public async Task<T> Save(T entity, CancellationToken cancellation = default)
        {
            EntityEntry<T> entityEntry = await _dbSet.AddAsync(entity);
            await _saveChangesAsync(cancellation);
            return entityEntry.Entity;
        }

        public async Task<T> Update(T entity, CancellationToken cancellation = default)
        {
            EntityEntry<T> entityEntry = _entry(entity);
            entityEntry.State = EntityState.Modified;
            await _saveChangesAsync(cancellation);
            return entity;
        }
    }
}

