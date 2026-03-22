using System.ComponentModel.DataAnnotations;

namespace MyLitleProgram.Application.Dtos.Author
{
    public class CreateAuthorDto
    {
        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [MaxLength(100, ErrorMessage = "El nombre no puede superar los 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "El email es obligatorio.")]
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [MaxLength(150, ErrorMessage = "El email no puede superar los 150 caracteres.")]
        public string Email { get; set; } = string.Empty;
    }
}
