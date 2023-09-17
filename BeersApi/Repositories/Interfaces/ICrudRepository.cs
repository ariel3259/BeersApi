using BeersApi.Models.Abstractions;

namespace BeersApi.Repositories.Interfaces
{
    public interface ICrudRepository<T>: IRepository<T> where T: BaseEntity
    {
        public Task<T> Save(T entity, CancellationToken cancellation = default);
        public Task<T> Update(T entity, CancellationToken cancellation = default);
        public Task<bool> Delete(Guid id, CancellationToken cancellation = default);
    }
}
