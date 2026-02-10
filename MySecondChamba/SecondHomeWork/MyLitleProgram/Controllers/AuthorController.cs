using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLittleProgram.Data;
using MyLittleProgram.Dtos;
using MyLittleProgram.Models;

namespace MyLittleProgram.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthorsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Authors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AuthorDto>>> GetAuthors()
        {
            var authors = await _context.Authors.ToListAsync();

            // Mapear Entity → DTO
            var authorDtos = authors.Select(a => new AuthorDto
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email
            }).ToList();

            return authorDtos;
        }

        // GET: api/Authors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AuthorDto>> GetAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);

            if (author == null)
            {
                return NotFound();
            }

            // Mapear Entity → DTO
            var authorDto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email
            };

            return authorDto;
        }

        // POST: api/Authors
        [HttpPost]
        public async Task<ActionResult<AuthorDto>> PostAuthor(CreateAuthorDto createDto)
        {
            // Mapear DTO → Entity
            var author = new Author
            {
                Name = createDto.Name,
                Email = createDto.Email
            };

            _context.Authors.Add(author);
            await _context.SaveChangesAsync();

            // Mapear Entity → DTO para respuesta
            var authorDto = new AuthorDto
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email
            };

            return CreatedAtAction(nameof(GetAuthor), new { id = author.Id }, authorDto);
        }

        // PUT: api/Authors/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAuthor(int id, UpdateAuthorDto updateDto)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            // Actualizar solo los campos del DTO
            author.Name = updateDto.Name;
            author.Email = updateDto.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AuthorExists(id))
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

        // DELETE: api/Authors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.Id == id);
        }
    }
}