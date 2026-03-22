using MyLitleProgram.Application.Core;
using MyLitleProgram.Application.Dtos.Post;

namespace MyLitleProgram.Application.Contract
{
    public interface IPostService
        : IBaseService<PostDto, CreatePostDto, UpdatePostDto>
    {
        Task<ServiceResult<IEnumerable<PostDto>>> GetByAuthorIdAsync(int authorId);
    }
}
