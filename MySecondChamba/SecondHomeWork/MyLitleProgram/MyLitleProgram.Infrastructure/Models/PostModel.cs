namespace MyLitleProgram.Infrastructure.Models
{
    public class PostModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int AuthorId { get; set; }
    }
}
