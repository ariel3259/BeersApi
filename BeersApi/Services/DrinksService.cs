using BeersApi.Dto;
using BeersApi.Services.Interfaces;

namespace BeersApi.Services
{
    public class DrinksService : IDrinksService
    {
        public Task<bool> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Pages<DrinksResponse>> GetAll(int? offset, int? limit)
        {
            throw new NotImplementedException();
        }

        public Task<DrinksResponse?> GetOne(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<DrinksResponse?> Save(DrinksRequest dto)
        {
            throw new NotImplementedException();
        }

        public Task<DrinksResponse?> Update(DrinksUpdate dto)
        {
            throw new NotImplementedException();
        }
    }
}
