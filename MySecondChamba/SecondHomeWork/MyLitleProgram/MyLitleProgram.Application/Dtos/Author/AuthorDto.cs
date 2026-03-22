using MyLitleProgram.Application.Dtos;

namespace MyLitleProgram.Application.Dtos.Author
{
    public class AuthorDto : DtoBase
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
