using BeersApi.Context;
using BeersApi.Models;
using BeersApi.Repositories.Abstractions;

namespace BeersApi.Repositories
{
    public class DrinksCrudRepository: CrudRepository<Drinks>
    {
        public DrinksCrudRepository(ApplicationContext context): base(context.Drinks, context.SaveChangesAsync, context.Entry)
        { }
    }
}
