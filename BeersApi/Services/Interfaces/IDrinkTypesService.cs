using BeersApi.Dto;
using BeersApi.Models;

namespace BeersApi.Services.Interfaces
{
    public interface IDrinkTypesService
    {
        public Task<Pages<DrinkTypesResponse>> GetAll(int? offset, int? limit);
    }
}
