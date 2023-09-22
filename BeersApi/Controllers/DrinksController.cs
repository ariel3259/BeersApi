using BeersApi.Dto;
using BeersApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BeersApi.Controllers
{
    [ApiController]
    [Route("/api/drinks")]
    public class DrinksController: ControllerBase
    {
        private readonly IDrinksService _service;
        
        public DrinksController(IDrinksService service)
        {
            _service = service;
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll([FromQuery(Name = "offset")] int? offset, [FromQuery(Name = "limit")] int? limit)
        {
            Pages<DrinksResponse> page = await _service.GetAll(offset, limit);
            Response.Headers.Add("x-total-count", page.TotalItems.ToString());
            return Ok(page.Elements);
        }


        [HttpPost()]
        public async Task<IActionResult> Save([FromBody] DrinksRequest dto)
        {
            DrinksResponse? drink = await _service.Save(dto);
            if (drink == null) return BadRequest();
            return Created("/api/products", drink);
        }

        [HttpPut("{id:Guid}")]
        public async Task<IActionResult> Update([FromBody] DrinksUpdate dto, [FromRoute(Name = "id")] Guid id)
        {
            DrinksResponse? response = await _service.Update(dto, id);
            if (response == null) return BadRequest();
            return Ok(response);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete([FromRoute(Name ="id")] Guid id)
        {
            bool result = await _service.Delete(id);
            if (!result) return BadRequest();
            return NoContent();
        }
    }
}
