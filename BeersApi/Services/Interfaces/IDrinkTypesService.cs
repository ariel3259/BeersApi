using BeersApi.Dto;

namespace BeersApi.Services.Interfaces
{
    public interface IDrinkTypesService
    {
        public Task<Pages<DrinkTypesResponse>> GetAll(int? offset, int? limit);
    }
}
