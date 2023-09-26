namespace BeersApi.Dto
{
    public class DrinksResponse
    {
        public int DrinksId { get; set; }
        public string Name { get; set; }
        public double AlcoholRate { get; set; }
        public int Price { get; set; }
        public int DrinkTypeId { get; set; }
    }
}
