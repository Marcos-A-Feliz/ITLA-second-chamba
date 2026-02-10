namespace MyLittleProgram.Dtos
{
    public class DeletePostDto
    {
        public bool ConfirmDelete { get; set; } = false;
        public string? Reason { get; set; }
        public bool DeletePermanently { get; set; } = true;
    }
}
