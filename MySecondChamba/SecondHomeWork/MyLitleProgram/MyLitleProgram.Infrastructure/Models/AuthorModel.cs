namespace MyLitleProgram.Infrastructure.Models
{
    public class AuthorModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public List<PostModel> Posts { get; set; } = new List<PostModel>();
    }
}
