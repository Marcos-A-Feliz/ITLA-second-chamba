using System.ComponentModel.DataAnnotations;

namespace MyLitleProgram.Application.Dtos.Post
{
    public class DeletePostDto
    {
        [Range(typeof(bool), "true", "true", ErrorMessage = "Debe confirmar la eliminación estableciendo ConfirmDelete en true.")]
        public bool ConfirmDelete { get; set; } = false;

        [MaxLength(300, ErrorMessage = "La razón no puede superar los 300 caracteres.")]
        public string? Reason { get; set; }

        public bool DeletePermanently { get; set; } = true;
    }
}
