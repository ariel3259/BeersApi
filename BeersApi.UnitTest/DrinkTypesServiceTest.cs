using BeersApi.Context;
using BeersApi.Dto;
using BeersApi.Models;
using BeersApi.Repositories;
using BeersApi.Repositories.Interfaces;
using BeersApi.Services;
using BeersApi.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Xunit;
using Moq;

namespace BeersApi.UnitTest
{
    [Collection("Drink Types")]
    public class DrinkTypesServiceTest
    {
        private readonly Mock<ApplicationContext> _appContextMock;
        private readonly IRepository<DrinkTypes> _repository;
        private readonly IDrinkTypesService _drinkTypesService;
        
        public DrinkTypesServiceTest()
        {
            IQueryable<DrinkTypes> drinkTypes = new List<DrinkTypes>()
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
            }.AsQueryable();
            _appContextMock = new Mock<ApplicationContext>();
            _appContextMock.Setup(x => x.Set<DrinkTypes>()).Returns(MockDbSet(drinkTypes));
            _repository = new DrinkTypesRepository(_appContextMock.Object);
            _drinkTypesService = new DrinkTypesService(_repository);
        }

        [Fact]
        public async void GetAllDrinkTypes()
        {
            Pages<DrinkTypesResponse> drinkTypes = await _drinkTypesService.GetAll(0, 10);
            Assert.Equal(10, drinkTypes.TotalItems);
            Assert.Equal("Whisky", drinkTypes.Elements[0].Description);
            Assert.Equal("Cerveza", drinkTypes.Elements[1].Description);
            Assert.Equal("Licor", drinkTypes.Elements[2].Description);
        }

        public DbSet<T> MockDbSet<T>(IQueryable<T> data) where T : class 
        {
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(data.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(data.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(data.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(data.GetEnumerator());

            return dbSetMock.Object;
        }
    }
}