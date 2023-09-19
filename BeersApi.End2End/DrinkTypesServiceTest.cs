using BeersApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace BeersApi.End2End
{
    public class DrinkTypesServiceTest
    {
        private readonly HttpClient _client;
        public DrinkTypesServiceTest()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:5247")
            };
        }
        [Fact]
        public async Task GetAllDrinksTypes()
        {
           HttpResponseMessage response = await _client.GetAsync("/api/drinkTypes");
            Assert.True((int)response.StatusCode == 200);
           
           List<DrinkTypesResponse>? list = await response.Content.ReadFromJsonAsync<List<DrinkTypesResponse>>();
            Assert.NotNull(list);
            Assert.True(list.Count <= 10);
        }
    }
}