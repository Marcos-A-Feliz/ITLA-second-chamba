namespace MyLittleProgram.Dtos
{
    public class UpdatePostDto
    {
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime? CreatedDate { get; set; } 
        public int AuthorId { get; set; }
    }
}
