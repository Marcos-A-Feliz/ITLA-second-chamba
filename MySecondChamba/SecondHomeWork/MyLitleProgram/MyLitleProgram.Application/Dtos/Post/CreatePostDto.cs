using System.ComponentModel.DataAnnotations;

namespace MyLitleProgram.Application.Dtos.Post
{
    public class CreatePostDto
    {
        [Required(ErrorMessage = "El título es obligatorio.")]
        [MaxLength(200, ErrorMessage = "El título no puede superar los 200 caracteres.")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "El contenido es obligatorio.")]
        public string Content { get; set; } = string.Empty;

        [Range(1, int.MaxValue, ErrorMessage = "El AuthorId debe ser un valor válido mayor a 0.")]
        public int AuthorId { get; set; }
    }
}
