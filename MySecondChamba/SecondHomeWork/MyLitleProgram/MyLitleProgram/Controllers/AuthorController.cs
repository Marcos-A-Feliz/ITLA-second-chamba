using Microsoft.AspNetCore.Mvc;
using MyLitleProgram.Application.Contract;
using MyLitleProgram.Application.Dtos.Author;

namespace MyLitleProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorsController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<IActionResult> GetAuthors()
        {
            var result = await _authorService.GetAllAsync();
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var result = await _authorService.GetByIdAsync(id);
            if (!result.Success)
                return NotFound(result.Errors);

            return Ok(result.Data);
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<IActionResult> PostAuthor([FromBody] CreateAuthorDto createDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authorService.CreateAsync(createDto);
            if (!result.Success)
                return BadRequest(result.Errors);

            return CreatedAtAction(nameof(GetAuthor), new { id = result.Data!.Id }, result.Data);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, [FromBody] UpdateAuthorDto updateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authorService.UpdateAsync(id, updateDto);
            if (!result.Success)
                return BadRequest(result.Errors);

            return Ok(result.Data);
        }

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAsync(id);
            if (!result.Success)
                return NotFound(result.Errors);

            return Ok(result.Message);
        }
    }
}
