using BeersApi.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace BeersApi.IntegrationTest
{
    public class DrinkTypesServiceTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;
        public DrinkTypesServiceTest(WebApplicationFactory<Program> testFactory)
        {
            _factory = testFactory;
        }
        [Fact]

        public async Task GetAllDrinksTypes()
        {
           string url = "/api/drinkTypes?offset";

           HttpClient client = _factory.CreateClient();
            HttpResponseMessage response = await client.GetAsync(url);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}