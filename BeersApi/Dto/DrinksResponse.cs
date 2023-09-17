namespace BeersApi.Dto
{
    public class DrinksResponse
    {
        public Guid DrinksId { get; set; }
        public string Name { get; set; }
        public int AlcoholRate { get; set; }
        public int Price { get; set; }
        public Guid DrinkTypeId { get; set; }
    }
}
