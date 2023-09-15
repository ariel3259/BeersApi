using BeersApi.Dto;
using BeersApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeersApi.Controllers
{
    [ApiController]
    [Route("/api/drinkTypes")]
    public class DrinkTypesController: ControllerBase
    {
        private readonly IBeerTypesService _service;
        public DrinkTypesController(IBeerTypesService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(int? offset, int? limit)
        {
            Pages<DrinkTypesResponse> page = await _service.GetAll(offset, limit);
            Response.Headers.Add("x-total-count", page.TotalItems.ToString());
            return Ok(page.Elements);
        }
    }
}
