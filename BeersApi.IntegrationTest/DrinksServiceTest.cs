using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BeersApi.Dto;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;

namespace BeersApi.IntegrationTest
{
    [Collection("drinks")]
    public class DrinksServiceTest: IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _testFactory;
        private Guid? _drinkToDelete;
        public DrinksServiceTest(WebApplicationFactory<Program> testFactory)
        {
            _testFactory = testFactory;
        }

        [Fact]
        public async Task GetAllDrinks()
        {
            HttpClient client = _testFactory.CreateClient();
            HttpResponseMessage response = await client.GetAsync("/api/drinks");
            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            List<DrinksResponse>? drinks = await response.Content.ReadFromJsonAsync<List<DrinksResponse>>();
            Assert.NotNull(drinks);
            Assert.True(drinks.Count <= 10);
        }

        [Fact]
        public async Task SaveDrinks()
        {
            DrinksRequest dto = new()
            {
                Name = "Imperial Lagger",
                AlcoholRate = 3.4,
                Price = 500,
                DrinkTypeId = Guid.Parse("72AA2673-8E12-4396-B632-2FBACD2F56C8")
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/drinks", content);
            Assert.Equal(201, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
            DrinksResponse drinksResponse = await response.Content.ReadFromJsonAsync<DrinksResponse>();
            _drinkToDelete = drinksResponse.DrinksId;
        }

        [Fact]
        public async Task SaveDrinks_Failure()
        {
            DrinksRequest dto = new()
            {
                Name = "Imperial Lagger",
                AlcoholRate = 3.4,
                DrinkTypeId = Guid.Parse("72AA2673-8E12-4396-B632-2FBACD2F56C8")
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/drinks", content);
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact(Timeout = 5000)]
        public async Task UpdateDrinks()
        {

            DrinksUpdate dto = new()
            {
                AlcoholRate = 5.5
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/api/drinks/{_drinkToDelete}", content);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact(Timeout = 7500)]
        public async Task T00005_DeleteDrinks()
        {
            HttpClient client = _testFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync($"/api/drinks/{_drinkToDelete}");
            Assert.Equal(204, (int)response.StatusCode);
        }
    }
}
