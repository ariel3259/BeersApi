using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services;
using BeersApi.Services.Interfaces;
using Moq;

namespace BeersApi.UnitTest
{
    [Collection("Drink Types")]
    public class DrinkTypesServiceTest
    {
        private readonly Mock<IRepository<DrinkTypes>> _repositoryMock;
        private readonly IDrinkTypesService _drinkTypesService;
        
        public DrinkTypesServiceTest()
        {
            _repositoryMock = new Mock<IRepository<DrinkTypes>>();
            Pages<DrinkTypes> page = new Pages<DrinkTypes>()
            {
                Elements = new List<DrinkTypes>()
                {
                    new DrinkTypes()
                    {
                        Description = "Whisky",
                    },
                    new DrinkTypes()
                    {
                        Description = "Cerveza"
                    },
                    new DrinkTypes()
                    {
                        Description = "Licor"
                    }
                },
                TotalItems = 3
            };
            _repositoryMock.Setup(x => x.GetAll(0, 10)).ReturnsAsync(page);
            _drinkTypesService = new DrinkTypesService(_repositoryMock.Object);
        }

        [Fact]
        public async void GetAllDrinkTypes()
        {
            Pages<DrinkTypesResponse> drinkTypes = await _drinkTypesService.GetAll(0, 10);
            Assert.Equal(3, drinkTypes.TotalItems);
            Assert.Equal("Whisky", drinkTypes.Elements[0].Description);
            Assert.Equal("Cerveza", drinkTypes.Elements[1].Description);
            Assert.Equal("Licor", drinkTypes.Elements[2].Description);
        }
    }
}