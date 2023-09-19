using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services;
using BeersApi.Services.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersApi.UnitTest
{
    [Collection("Drinks")]
    public class DrinksServiceTest
    {
        private readonly Mock<ICrudRepository<Drinks>> _repositoryDrinksMock;
        private readonly Mock<ICrudRepository<DrinkTypes>> _repositoryDrinkTypesMock;
        private readonly IDrinksService _service;

        public DrinksServiceTest()
        {
            _repositoryDrinksMock = new Mock<ICrudRepository<Drinks>>();
            _repositoryDrinkTypesMock = new Mock<ICrudRepository<DrinkTypes>>();

            DrinkTypes drinkType = new DrinkTypes()
            {
                Id = Guid.Parse("f8d6aa95-25ca-4846-b265-97bede91b00a"),
                Description = "Cerveza"
            };
            List<Drinks> drinks = new List<Drinks>()
            {
                new Drinks()
                {
                     AlcoholRate= 6,
                     DrinkType = drinkType,
                     Name = "Negra",
                     Price = 750
                },
                new Drinks()
                {
                     AlcoholRate= 3,
                     DrinkType = drinkType,
                     Name = "Rubia",
                     Price = 750
                }
            };
            _repositoryDrinkTypesMock.Setup((x) => x.GetById(Guid.Parse("f8d6aa95-25ca-4846-b265-97bede91b00a"))).ReturnsAsync(drinkType);
            _repositoryDrinksMock.Setup((x) => x.GetAll(0, 10)).ReturnsAsync(new Dto.Pages<Drinks>() {
                Elements = drinks,
                TotalItems = 10
            });
            _service = new DrinksService(_repositoryDrinksMock.Object, _repositoryDrinkTypesMock.Object);

        }

    }
}
