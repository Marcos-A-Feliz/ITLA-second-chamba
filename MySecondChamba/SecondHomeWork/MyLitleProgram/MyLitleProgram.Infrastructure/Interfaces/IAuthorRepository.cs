using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Models;

namespace MyLitleProgram.Infrastructure.Interfaces
{
    public interface IAuthorRepository
    {
        Task<IEnumerable<AuthorModel>> GetAllAsync();
        Task<AuthorModel?> GetByIdAsync(int id);
        Task<Author> AddAsync(Author author);
        Task UpdateAsync(int id, Author author);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
