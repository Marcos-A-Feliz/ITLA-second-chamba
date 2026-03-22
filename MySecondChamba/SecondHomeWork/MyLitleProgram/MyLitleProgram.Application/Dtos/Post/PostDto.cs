using MyLitleProgram.Application.Dtos;

namespace MyLitleProgram.Application.Dtos.Post
{
    public class PostDto : DtoBase
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public int AuthorId { get; set; }
    }
}
