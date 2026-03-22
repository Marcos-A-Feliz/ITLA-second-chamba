using MyLitleProgram.Domain.Core;
using System.Text.Json.Serialization;

namespace MyLitleProgram.Domain.Entities
{
    public class Author : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        [JsonIgnore]
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
