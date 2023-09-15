using BeersApi.Dto;

namespace BeersApi.Services.Interfaces
{
    public interface IBeerTypesService
    {
        public Task<Pages<DrinkTypesResponse>> GetAll(int? offset, int? limit);
    }
}
