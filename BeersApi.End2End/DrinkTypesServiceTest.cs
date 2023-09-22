using BeersApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace BeersApi.End2End
{
    public class DrinkTypesServiceTest
    {
        private readonly WebApplicationFactory<Program> _appFactory;
        public DrinkTypesServiceTest()
        {
            _appFactory = new WebApplicationFactory<Program>();
        }
        [Fact]
        public async Task GetAllDrinksTypes()
        {
           HttpClient client = _appFactory.CreateClient();
           HttpResponseMessage response = await client.GetAsync("/api/drinkTypes");
            Assert.True((int)response.StatusCode == 200);
           
           List<DrinkTypesResponse>? list = await response.Content.ReadFromJsonAsync<List<DrinkTypesResponse>>();
            Assert.NotNull(list);
            Assert.True(list.Count <= 10);
        }
    }
}