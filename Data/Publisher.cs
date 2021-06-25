using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace aspnetcore_redis_cache.Data
{
    public class Publisher
    {
        public int PublisherId { get; set; }
        public string Name { get; set; }
        [JsonIgnore]
        public virtual ICollection<Book> Books { get; set; }
    }
}