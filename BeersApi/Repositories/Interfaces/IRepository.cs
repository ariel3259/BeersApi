using BeersApi.Dto;
using BeersApi.Models.Abstracts;

namespace BeersApi.Repositories.Interfaces
{
    public interface IRepository<T> where T: BaseEntity
    {
        public Task<List<T>> GetAll();
        public Task<Pages<T>> GetAll(int? offset, int? limit);
    }
}
