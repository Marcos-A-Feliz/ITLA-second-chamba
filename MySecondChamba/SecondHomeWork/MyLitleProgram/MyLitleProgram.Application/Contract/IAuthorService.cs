using MyLitleProgram.Application.Core;
using MyLitleProgram.Application.Dtos.Author;

namespace MyLitleProgram.Application.Contract
{
    public interface IAuthorService
        : IBaseService<AuthorDto, CreateAuthorDto, UpdateAuthorDto>
    {
        // Métodos adicionales específicos de Author si se necesitan en el futuro
    }
}
