using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using BeersApi.Dto;
using Microsoft.AspNetCore.Mvc.Testing;

namespace BeersApi.End2End
{
    [Collection("drinks")]
    public class DrinksServiceTest
    {
        private readonly WebApplicationFactory<Program> _appProgram;

        public DrinksServiceTest()
        {
            _appProgram = new WebApplicationFactory<Program>();
        }

        [Fact]
        public async Task GetAllDrinks()
        {
            HttpClient client = _appProgram.CreateClient();
            HttpResponseMessage response = await client.GetAsync("/api/drinks");
            List<DrinksResponse>? drinks = await response.Content.ReadFromJsonAsync<List<DrinksResponse>>(); 
            Assert.Equal(200, (int)response.StatusCode);
            Assert.NotNull(drinks);
            Assert.Equal(10, drinks.Count);
        }
    }
}
