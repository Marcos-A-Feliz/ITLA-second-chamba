using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Models;

namespace MyLitleProgram.Infrastructure.Interfaces
{
    public interface IPostRepository
    {
        Task<IEnumerable<PostModel>> GetAllAsync();
        Task<PostModel?> GetByIdAsync(int id);
        Task<IEnumerable<PostModel>> GetByAuthorIdAsync(int authorId);
        Task<Post> AddAsync(Post post);
        Task UpdateAsync(int id, Post post);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
