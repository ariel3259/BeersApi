using BeersApi.Context;
using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BeersApi.Repositories
{
    public class DrinksRepository: ICrudRepository<Drinks>
    {
        private readonly ApplicationContext _appContext;

        public DrinksRepository(ApplicationContext appContext)
        {
            _appContext = appContext;
        }

        public Task<Drinks> Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Drinks>> GetAll()
        {
            return await _appContext.Drinks.Where(x => x.Status).ToListAsync();
        }

        public async Task<Pages<Drinks>> GetAll(int? offset, int? limit)
        {
            Expression<Func<Drinks, bool>> predicate = x => x.Status;
            List<Drinks> beers = await _appContext.Drinks.Where(predicate).OrderBy(x => x.Name).Skip(offset ?? 0).Take(limit ?? 10).ToListAsync();
            int totalItems = await _appContext.Drinks.Where(predicate).CountAsync();
            throw new NotImplementedException();
        }

        public Task<Drinks> Save(Drinks entity)
        {
            throw new NotImplementedException();
        }

        public Task<Drinks> Update(Drinks entity)
        {
            throw new NotImplementedException();
        }
    }
}
