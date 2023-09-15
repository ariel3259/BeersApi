using BeersApi.Context;
using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeersApi.Repositories
{
    public class DrinksTypesRepository: IRepository<DrinkTypes>
    {
        private readonly ApplicationContext _appContext;

        public DrinksTypesRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
            string[] descriptions = { "Whisky", "Cerveza", "Licor", "Vodka" };
            List<DrinkTypes> beerTypes = descriptions
                .Where(x => !_appContext.DrinkTypes.Any(y => y.Description == x))
                .Select(x => new DrinkTypes
                {
                    Description = x
                }).ToList();
            if (beerTypes.Count > 0)
            {
                _appContext.DrinkTypes.AddRange(beerTypes);
                _appContext.SaveChanges();
            }
        }

        public async Task<List<DrinkTypes>> GetAll()
        {
            return await _appContext.DrinkTypes.Where(x => x.Status).ToListAsync();
        }

        public async Task<Pages<DrinkTypes>> GetAll(int? offset, int? limit)
        {
            Expression<Func<DrinkTypes, bool>> predicate = x => x.Status;
            List<DrinkTypes> beerTypes = await _appContext.DrinkTypes.Where(x => x.Status).OrderBy(x => x.Description).Skip(offset ?? 0).Take(limit ?? 10).ToListAsync();
            int totalItems = await _appContext.DrinkTypes.CountAsync();
            return new Pages<DrinkTypes>()
            {
                Elements = beerTypes,
                TotalItems = totalItems,
            };
        }
    }
}
