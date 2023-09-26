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
    public class DrinksServiceTest: IClassFixture<TestWebApplicationFactory<Program>>
    {
        private readonly TestWebApplicationFactory<Program> _testFactory;
        public DrinksServiceTest(TestWebApplicationFactory<Program> testFactory)
        {
            _testFactory = testFactory;
        }

        [Fact]
        public async Task T1_GetAllDrinks()
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
        public async Task T2_SaveDrinks()
        {
            DrinksRequest dto = new()
            {
                Name = "Imperial Lagger",
                AlcoholRate = 3.4,
                Price = 500,
                DrinkTypeId = 3
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/drinks", content);
            Assert.Equal(201, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task T3_SaveDrinks_FailArgumentRequired()
        {
            DrinksRequest dto = new()
            {
                Name = "Imperial Lagger",
                AlcoholRate = 3.4,
                DrinkTypeId = 3
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/drinks", content);
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task T4_SaveDrinks_Fail_DrinkTypeSelectedDoesNotExists()
        {
            DrinksRequest dto = new()
            {
                Name = "Imperial Lagger",
                AlcoholRate = 3.4,
                Price = 500,
                DrinkTypeId = 6
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PostAsync("/api/drinks", content);
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task T5_UpdateDrinks()
        {
            int id = 1;
            DrinksUpdate dto = new()
            {
                AlcoholRate = 5.5
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/api/drinks/{id}", content);
            Assert.Equal(200, (int)response.StatusCode);
            Assert.Equal("application/json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task T6_UpdateDrinks_FailDrinkDoesNotExits()
        {
            int id = 2;
            DrinksUpdate dto = new()
            {
                AlcoholRate = 5.5
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/api/drinks/{id}", content);
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task T7_UpdateDrinks_FailDrinkTypeSelectedDoesNotExits()
        {
            int id = 1;
            DrinksUpdate dto = new()
            {
                AlcoholRate = 5.5,
                DrinkTypeId = 6
            };
            HttpClient client = _testFactory.CreateClient();
            string json = JsonConvert.SerializeObject(dto);
            StringContent content = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await client.PutAsync($"/api/drinks/{id}", content);
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task T9_DeleteDrinks()
        {
            int id = 1;
            HttpClient client = _testFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync($"/api/drinks/{id}");
            Assert.Equal(204, (int)response.StatusCode);
        }

        [Fact]
        public async Task T9_DeleteDrinks_Failed()
        {
            int id = 2;
            HttpClient client = _testFactory.CreateClient();
            HttpResponseMessage response = await client.DeleteAsync($"/api/drinks/{id}");
            Assert.Equal(400, (int)response.StatusCode);
            Assert.Equal("application/problem+json; charset=utf-8", response.Content.Headers.ContentType.ToString());
        }
    }
}
