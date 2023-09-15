using BeersApi.Models.Abstracts;

namespace BeersApi.Repositories.Interfaces
{
    public interface ICrudRepository<T>: IRepository<T> where T: BaseEntity
    {
        Task<T> Save(T entity);
        Task<T> Update(T entity);
        Task<T> Delete(Guid id);
    }
}
