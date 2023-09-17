using BeersApi.Dto;
using BeersApi.Models.Abstractions;

namespace BeersApi.Repositories.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        public Task<T?> GetById(Guid id);
        public Task<List<T>> GetAll();
        public Task<Pages<T>> GetAll(int? offset, int? limit);
    }
}
