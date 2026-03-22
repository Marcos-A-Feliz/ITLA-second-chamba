using Microsoft.EntityFrameworkCore;
using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Context;
using MyLitleProgram.Infrastructure.Core;
using MyLitleProgram.Infrastructure.Exceptions;
using MyLitleProgram.Infrastructure.Interfaces;
using MyLitleProgram.Infrastructure.Models;

namespace MyLitleProgram.Infrastructure.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<AuthorModel>> GetAllAsync()
        {
            var authors = await _dbSet.Include(a => a.Posts).ToListAsync();

            return authors.Select(a => new AuthorModel
            {
                Id = a.Id,
                Name = a.Name,
                Email = a.Email,
                Posts = a.Posts.Select(p => new PostModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    AuthorId = p.AuthorId
                }).ToList()
            });
        }

        public async Task<AuthorModel?> GetByIdAsync(int id)
        {
            var author = await _dbSet.Include(a => a.Posts)
                                     .FirstOrDefaultAsync(a => a.Id == id);

            if (author == null)
                return null;

            return new AuthorModel
            {
                Id = author.Id,
                Name = author.Name,
                Email = author.Email,
                Posts = author.Posts.Select(p => new PostModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    CreatedDate = p.CreatedDate,
                    AuthorId = p.AuthorId
                }).ToList()
            };
        }

        public async Task<Author> AddAsync(Author author)
        {
            try
            {
                await _dbSet.AddAsync(author);
                await _context.SaveChangesAsync();
                return author;
            }
            catch (Exception ex)
            {
                throw new AuthorException("Error al crear el autor.", ex);
            }
        }

        public async Task UpdateAsync(int id, Author author)
        {
            var existing = await _dbSet.FindAsync(id)
                ?? throw new AuthorException($"Autor con Id {id} no encontrado.");

            existing.Name = author.Name;
            existing.Email = author.Email;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AuthorException("Error al actualizar el autor.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var author = await _dbSet.FindAsync(id)
                ?? throw new AuthorException($"Autor con Id {id} no encontrado.");

            try
            {
                _dbSet.Remove(author);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new AuthorException("Error al eliminar el autor.", ex);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(a => a.Id == id);
        }
    }
}
