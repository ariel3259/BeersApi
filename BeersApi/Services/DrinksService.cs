using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services.Interfaces;
using System.Linq.Expressions;

namespace BeersApi.Services
{
    public class DrinksService : IDrinksService
    {
        private readonly ICrudRepository<Drinks> _drinksRepository;
        private readonly IRepository<DrinkTypes> _drinkTypesRepository;
        public DrinksService(ICrudRepository<Drinks> drinksRepository, IRepository<DrinkTypes> drinkTypesRepository)
        {
            _drinksRepository = drinksRepository;
            _drinkTypesRepository = drinkTypesRepository;
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

        public async Task<DrinksResponse?> Save(DrinksRequest dto)
        {
            DrinkTypes drinkType = await _drinkTypesRepository.GetById(dto.DrinkTypeId);
            if (drinkType == null) return null;
            Drinks drink = new()
            {
                Name = dto.Name,
                AlcoholRate = dto.AlcoholRate,
                DrinkTypeId = dto.DrinkTypeId,
                Price = dto.Price
            };
            Drinks response = await _drinksRepository.Save(drink);
            return new DrinksResponse()
            {
                DrinksId = response.Id,
                Name = response.Name,
                AlcoholRate = response.AlcoholRate,
                DrinkTypeId = response.DrinkTypeId,
                Price = response.Price
            };
        }

        public async Task<DrinksResponse?> Update(DrinksUpdate dto, Guid id)
        {
            Drinks? drink = await _drinksRepository.GetById(id);
            if (drink == null) return null;
            if (dto.DrinkTypeId != null)
            {
                DrinkTypes? drinkType = await _drinkTypesRepository.GetById((Guid)dto.DrinkTypeId);
                if (drinkType == null) return null;
                else drink.DrinkTypeId = dto.DrinkTypeId != drink.DrinkTypeId ? (Guid)dto.DrinkTypeId : drink.DrinkTypeId;
            }
            drink.AlcoholRate = dto.AlcoholRate != null && dto.AlcoholRate != drink.AlcoholRate ? (int)dto.AlcoholRate : drink.AlcoholRate;
            drink.Price = dto.Price != null && dto.Price != drink.Price ? (int)dto.Price : drink.Price;
            drink.Name = dto.Name != null && dto.Name != drink.Name ? (string)dto.Name : drink.Name;
            drink.UpdatedAt = DateTime.UtcNow;
            await _drinksRepository.Update(drink);
            return new DrinksResponse()
            {
                DrinksId = drink.Id,
                Name = drink.Name,
                AlcoholRate = drink.AlcoholRate,
                DrinkTypeId = drink.DrinkTypeId,
                Price = drink.Price
            };
        }
    }
}
