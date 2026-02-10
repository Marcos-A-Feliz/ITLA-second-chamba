using MyLittleProgram.Models;
using System.Text.Json.Serialization;

public class Post
{
    public int Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;

    public int AuthorId { get; set; }

    [JsonIgnore]
    public Author? Author { get; set; }
}
