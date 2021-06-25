namespace aspnetcore_redis_cache.Data
{
    public class Book
    {
        public int BookId { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }

        public Publisher Publisher { get; set; }
        public int PublisherId { get; set; }
    }
}