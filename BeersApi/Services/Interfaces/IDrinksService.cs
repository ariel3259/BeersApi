using BeersApi.Dto;

namespace BeersApi.Services.Interfaces
{
    public interface IDrinksService
    {
        public Task<Pages<DrinksResponse>> GetAll(int? offset, int? limit);
        public Task<DrinksResponse?> GetOne(Guid id);
        public Task<DrinksResponse?> Save(DrinksRequest dto);
        public Task<DrinksResponse?> Update(DrinksUpdate dto);
        public Task<bool> Delete(Guid id);
    }
}
