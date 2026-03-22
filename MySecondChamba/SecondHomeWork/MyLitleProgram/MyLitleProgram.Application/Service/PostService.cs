using MyLitleProgram.Application.Contract;
using MyLitleProgram.Application.Core;
using MyLitleProgram.Application.Dtos.Post;
using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Interfaces;

namespace MyLitleProgram.Application.Service
{
    public class PostService : BaseService<PostDto, CreatePostDto, UpdatePostDto>, IPostService
    {
        private readonly IPostRepository _postRepository;
        private readonly IAuthorRepository _authorRepository;

        public PostService(IPostRepository postRepository, IAuthorRepository authorRepository)
        {
            _postRepository = postRepository;
            _authorRepository = authorRepository;
        }

        public override async Task<ServiceResult<IEnumerable<PostDto>>> GetAllAsync()
        {
            try
            {
                var posts = await _postRepository.GetAllAsync();

                var dtos = posts.Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    AuthorId = p.AuthorId
                });

                return Ok<IEnumerable<PostDto>>(dtos);
            }
            catch (Exception ex)
            {
                return Fail<IEnumerable<PostDto>>($"Error al obtener los posts: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<PostDto>> GetByIdAsync(int id)
        {
            try
            {
                var post = await _postRepository.GetByIdAsync(id);

                if (post == null)
                    return Fail<PostDto>($"No se encontró el post con Id {id}.");

                var dto = new PostDto
                {
                    Id = post.Id,
                    Title = post.Title,
                    Content = post.Content,
                    CreatedDate = post.CreatedDate,
                    AuthorId = post.AuthorId
                };

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return Fail<PostDto>($"Error al obtener el post: {ex.Message}");
            }
        }

        public async Task<ServiceResult<IEnumerable<PostDto>>> GetByAuthorIdAsync(int authorId)
        {
            try
            {
                // Validar que el autor existe
                if (!await _authorRepository.ExistsAsync(authorId))
                    return Fail<IEnumerable<PostDto>>($"No se encontró el autor con Id {authorId}.");

                var posts = await _postRepository.GetByAuthorIdAsync(authorId);

                var dtos = posts.Select(p => new PostDto
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    AuthorId = p.AuthorId
                });

                return Ok<IEnumerable<PostDto>>(dtos);
            }
            catch (Exception ex)
            {
                return Fail<IEnumerable<PostDto>>($"Error al obtener los posts del autor: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<PostDto>> CreateAsync(CreatePostDto createDto)
        {
            try
            {
                // Validación: Title obligatorio
                if (string.IsNullOrWhiteSpace(createDto.Title))
                    return Fail<PostDto>("El título es obligatorio.");

                if (createDto.Title.Length > 200)
                    return Fail<PostDto>("El título no puede superar los 200 caracteres.");

                // Validación: Content obligatorio
                if (string.IsNullOrWhiteSpace(createDto.Content))
                    return Fail<PostDto>("El contenido es obligatorio.");

                // Validación: AuthorId válido
                if (createDto.AuthorId <= 0)
                    return Fail<PostDto>("El AuthorId debe ser un valor válido mayor a 0.");

                if (!await _authorRepository.ExistsAsync(createDto.AuthorId))
                    return Fail<PostDto>($"No se encontró el autor con Id {createDto.AuthorId}.");

                var post = new Post
                {
                    Title = createDto.Title.Trim(),
                    Content = createDto.Content.Trim(),
                    AuthorId = createDto.AuthorId,
                    CreatedDate = DateTime.UtcNow
                };

                var created = await _postRepository.AddAsync(post);

                var dto = new PostDto
                {
                    Id = created.Id,
                    Title = created.Title,
                    Content = created.Content,
                    CreatedDate = created.CreatedDate,
                    AuthorId = created.AuthorId
                };

                return Ok(dto, "Post creado exitosamente.");
            }
            catch (Exception ex)
            {
                return Fail<PostDto>($"Error al crear el post: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<PostDto>> UpdateAsync(int id, UpdatePostDto updateDto)
        {
            try
            {
                if (!await _postRepository.ExistsAsync(id))
                    return Fail<PostDto>($"No se encontró el post con Id {id}.");

                // Validación: Title obligatorio
                if (string.IsNullOrWhiteSpace(updateDto.Title))
                    return Fail<PostDto>("El título es obligatorio.");

                if (updateDto.Title.Length > 200)
                    return Fail<PostDto>("El título no puede superar los 200 caracteres.");

                // Validación: Content obligatorio
                if (string.IsNullOrWhiteSpace(updateDto.Content))
                    return Fail<PostDto>("El contenido es obligatorio.");

                // Validación: AuthorId válido
                if (updateDto.AuthorId <= 0)
                    return Fail<PostDto>("El AuthorId debe ser un valor válido mayor a 0.");

                if (!await _authorRepository.ExistsAsync(updateDto.AuthorId))
                    return Fail<PostDto>($"No se encontró el autor con Id {updateDto.AuthorId}.");

                var post = new Post
                {
                    Title = updateDto.Title.Trim(),
                    Content = updateDto.Content.Trim(),
                    AuthorId = updateDto.AuthorId,
                    CreatedDate = updateDto.CreatedDate ?? DateTime.UtcNow
                };

                await _postRepository.UpdateAsync(id, post);

                var updated = await _postRepository.GetByIdAsync(id);

                var dto = new PostDto
                {
                    Id = updated!.Id,
                    Title = updated.Title,
                    Content = updated.Content,
                    CreatedDate = updated.CreatedDate,
                    AuthorId = updated.AuthorId
                };

                return Ok(dto, "Post actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return Fail<PostDto>($"Error al actualizar el post: {ex.Message}");
            }
        }

        public override async Task<ServiceResult<bool>> DeleteAsync(int id)
        {
            try
            {
                if (!await _postRepository.ExistsAsync(id))
                    return Fail<bool>($"No se encontró el post con Id {id}.");

                await _postRepository.DeleteAsync(id);
                return Ok(true, "Post eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return Fail<bool>($"Error al eliminar el post: {ex.Message}");
            }
        }
    }
}
