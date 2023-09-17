namespace BeersApi.Dto
{
    public class DrinksUpdate
    {
        public string? Name { get; set; }
        public int? Price { get; set; }
        public int? AlcoholRate { get; set; }
        public Guid? DrinkTypeId { get; set; } 
    }
}
