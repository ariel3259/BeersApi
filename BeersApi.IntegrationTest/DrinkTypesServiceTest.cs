using BeersApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace BeersApi.IntegrationTest
{
    public class DrinkTypesServiceTest : IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _testFactory;
        public DrinkTypesServiceTest(TestWebApplicationFactory<Program> testFactory)
        {
            _testFactory = testFactory;
        }
        [Fact]

        public async Task GetAllDrinksTypes()
        {
           string url = "/api/drinkTypes?offset";

           HttpClient client = _testFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(url);
            List<DrinkTypesResponse> drinkTypes = await response.Content.ReadFromJsonAsync<List<DrinkTypesResponse>>();
            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            Assert.Equal(5, drinkTypes.Count);
        }
    }
}