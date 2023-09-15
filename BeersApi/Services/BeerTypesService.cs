using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services.Interfaces;

namespace BeersApi.Services
{
    public class BeerTypesService : IBeerTypesService
    {
        private readonly IRepository<DrinkTypes> _repository;

        public BeerTypesService(IRepository<DrinkTypes> repository)
        {
            _repository = repository;
        }
        public async Task<Pages<DrinkTypesResponse>> GetAll(int? offset, int? limit)
        {
            Pages<DrinkTypes> beerTypes = await _repository.GetAll(offset, limit);
            List<DrinkTypesResponse> response = beerTypes.Elements.Select((x) => new DrinkTypesResponse()
            {
                BeerTypesId = x.Id,
                Description = x.Description
            }).ToList();
            return new Pages<DrinkTypesResponse>()
            {
                Elements = response,
                TotalItems = beerTypes.TotalItems
            };
        }
    }
}
