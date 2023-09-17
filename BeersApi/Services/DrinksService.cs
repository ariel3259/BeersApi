using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services.Interfaces;

namespace BeersApi.Services
{
    public class DrinksService : IDrinksService
    {
        public ICrudRepository<Drinks> _drinksRepository { get; set; }
        public DrinksService(ICrudRepository<Drinks> drinksRepository)
        {
            _drinksRepository = drinksRepository;
        }

        public async Task<bool> Delete(Guid id)
        {
            return await _drinksRepository.Delete(id);
        }

        public async Task<Pages<DrinksResponse>> GetAll(int? offset, int? limit)
        {
            Pages<Drinks> page = await _drinksRepository.GetAll(offset, limit);
            List<DrinksResponse> drinksResponse = page.Elements
                .Select(x => new DrinksResponse()
                {
                    DrinksId = x.Id,
                    Name = x.Name,
                    AlcoholRate = x.AlcoholRate,
                    DrinkTypeId = x.DrinkTypeId,
                    Price = x.Price
                }).ToList();
            return new Pages<DrinksResponse>()
            {
                Elements = drinksResponse,
                TotalItems = page.TotalItems,
            };
        }

        public async Task<DrinksResponse?> GetOne(Guid id)
        {
            Drinks? drink = await _drinksRepository.GetById(id);
            if (drink == null) return null;
            return new DrinksResponse()
            {
                DrinksId = drink.Id,
                Name = drink.Name,
                AlcoholRate = drink.AlcoholRate,
                DrinkTypeId = drink.DrinkTypeId,
                Price = drink.Price
            };
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
