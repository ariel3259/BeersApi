using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeersApi.End2End
{
    [Collection("drinks")]
    public class DrinksServiceTest
    {
        private readonly HttpClient _client;

        public DrinksServiceTest()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://localhost:5247")
            };
        }

        [Fact]
        public async Task GetAllDrinks()
        {
            
        }
    }
}
