using Microsoft.AspNetCore.Mvc;
using MyLitleProgram.Application.Contract;
using MyLitleProgram.Application.Dtos.Post;

namespace MyLitleProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostsController(IPostService postService)
        {
            _postService = postService;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<IActionResult> GetPosts()
        {
            var result = await _postService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        // GET: api/Posts/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPost(int id)
        {
            var result = await _postService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        // GET: api/Posts/author/5
        [HttpGet("author/{authorId}")]
        public async Task<IActionResult> GetPostsByAuthor(int authorId)
        {
            var result = await _postService.GetByAuthorIdAsync(authorId);
            if (!result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        // POST: api/Posts
        [HttpPost]
        public async Task<IActionResult> PostPost([FromBody] CreatePostDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _postService.CreateAsync(createDto);
            if (!result.Success)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetPost), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/Posts/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(int id, [FromBody] UpdatePostDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _postService.UpdateAsync(id, updateDto);
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        // DELETE: api/Posts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id, [FromBody] DeletePostDto? deleteDto = null)
        {
            if (deleteDto != null && !deleteDto.ConfirmDelete)
                return BadRequest("Debe confirmar la eliminación estableciendo ConfirmDelete en true.");

            if (deleteDto?.Reason != null)
                Console.WriteLine($"Post {id} eliminado. Razón: {deleteDto.Reason}");

            var result = await _postService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result.Errors);

            return Ok(result.Message);
        }
    }
}
