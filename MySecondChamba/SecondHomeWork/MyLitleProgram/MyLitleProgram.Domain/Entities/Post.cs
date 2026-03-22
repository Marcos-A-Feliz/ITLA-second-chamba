using MyLitleProgram.Domain.Core;
using System.Text.Json.Serialization;

namespace MyLitleProgram.Domain.Entities
{
    public class Post : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public int AuthorId { get; set; }

        [JsonIgnore]
        public Author? Author { get; set; }
    }
}
