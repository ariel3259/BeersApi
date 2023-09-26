using System.ComponentModel.DataAnnotations;

namespace BeersApi.Dto
{
    public class DrinksRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public double AlcoholRate { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int DrinkTypeId { get; set; }
    }
}
