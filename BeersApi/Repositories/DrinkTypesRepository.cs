using BeersApi.Context;
using BeersApi.Models;
using BeersApi.Repositories.Abstractions;

namespace BeersApi.Repositories
{
    public class DrinkTypesRepository: Repository<DrinkTypes>
    {

        public DrinkTypesRepository(ApplicationContext context): base(context.DrinkTypes)
        {
            /*string[] descriptions = { "Whisky", "Vodka", "Cerveza", "Gin", "Licor" };
            List<DrinkTypes> drinkTypes = descriptions
                .Where(x => !context.DrinkTypes.Where(y => y.Description == x).Any())
                .Select(x => new DrinkTypes()
                {
                    Description = x
                }).ToList();
            if (descriptions.Length > 0)
            {
                context.AddRange(drinkTypes);
                context.SaveChanges();
            }*/
        }
    }
}
