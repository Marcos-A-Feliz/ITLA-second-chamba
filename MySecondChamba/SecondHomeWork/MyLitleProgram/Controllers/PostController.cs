using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLittleProgram.Data;
using MyLittleProgram.Dtos;


namespace MyLittleProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PostsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPosts()
        {
            var posts = await _context.Posts.ToListAsync();

            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                AuthorId = p.AuthorId
            }).ToList();

            return postDtos;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PostDto>> GetPost(int id)
        {
            var post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedDate = post.CreatedDate,
                AuthorId = post.AuthorId
            };

            return postDto;
        }

        [HttpPost]
        public async Task<ActionResult<PostDto>> PostPost(CreatePostDto createDto)
        {
            var authorExists = await _context.Authors.AnyAsync(a => a.Id == createDto.AuthorId);
            if (!authorExists)
            {
                return BadRequest("El autor especificado no existe.");
            }

            var post = new Post
            {
                Title = createDto.Title,
                Content = createDto.Content,
                AuthorId = createDto.AuthorId,
                CreatedDate = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            await _context.SaveChangesAsync();

            var postDto = new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedDate = post.CreatedDate,
                AuthorId = post.AuthorId
            };

            return CreatedAtAction(nameof(GetPost), new { id = post.Id }, postDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, UpdatePostDto updateDto)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            if (post.AuthorId != updateDto.AuthorId)
            {
                var authorExists = await _context.Authors.AnyAsync(a => a.Id == updateDto.AuthorId);
                if (!authorExists)
                {
                    return BadRequest("El nuevo autor especificado no existe.");
                }
            }

            post.Title = updateDto.Title;
            post.Content = updateDto.Content;
            post.AuthorId = updateDto.AuthorId;

            if (updateDto.CreatedDate.HasValue)
            {
                post.CreatedDate = updateDto.CreatedDate.Value;
            }

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id, [FromBody] DeletePostDto? deleteDto = null)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            if (deleteDto != null)
            {
                if (!deleteDto.ConfirmDelete)
                {
                    return BadRequest("Debe confirmar la eliminación estableciendo ConfirmDelete en true");
                }

                if (!string.IsNullOrEmpty(deleteDto.Reason))
                {
                    Console.WriteLine($"Post {id} eliminado. Razón: {deleteDto.Reason}");
                }
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpGet("author/{authorId}")]
        public async Task<ActionResult<IEnumerable<PostDto>>> GetPostsByAuthor(int authorId)
        {
            var posts = await _context.Posts
                .Where(p => p.AuthorId == authorId)
                .ToListAsync();

            if (!posts.Any())
            {
                return NotFound();
            }

            var postDtos = posts.Select(p => new PostDto
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                AuthorId = p.AuthorId
            }).ToList();

            return postDtos;
        }

        private bool PostExists(int id)
        {
            return _context.Posts.Any(e => e.Id == id);
        }
    }
}