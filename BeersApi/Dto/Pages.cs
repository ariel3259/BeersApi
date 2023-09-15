namespace BeersApi.Dto
{
    public class Pages<T>
    {
        public List<T> Elements { get; set; }
        public int TotalItems { get; set; }
    }
}
