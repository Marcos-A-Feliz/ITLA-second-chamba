using Microsoft.Extensions.Hosting;
using System.Text.Json.Serialization;

namespace MyLittleProgram.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;


        [JsonIgnore]
        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
