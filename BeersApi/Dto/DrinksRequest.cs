using System.ComponentModel.DataAnnotations;

namespace BeersApi.Dto
{
    public class DrinksRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public int AlcoholRage { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public Guid DrinkTypeId { get; set; }
    }
}
