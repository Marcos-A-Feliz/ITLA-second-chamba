using Microsoft.EntityFrameworkCore;
using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Context;
using MyLitleProgram.Infrastructure.Core;
using MyLitleProgram.Infrastructure.Exceptions;
using MyLitleProgram.Infrastructure.Interfaces;
using MyLitleProgram.Infrastructure.Models;

namespace MyLitleProgram.Infrastructure.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {
        public PostRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            var posts = await _dbSet.ToListAsync();

            return posts.Select(p => new PostModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                AuthorId = p.AuthorId
            });
        }

        public async Task<PostModel?> GetByIdAsync(int id)
        {
            var post = await _dbSet.FindAsync(id);

            if (post == null)
                return null;

            return new PostModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedDate = post.CreatedDate,
                AuthorId = post.AuthorId
            };
        }

        public async Task<IEnumerable<PostModel>> GetByAuthorIdAsync(int authorId)
        {
            var posts = await _dbSet.Where(p => p.AuthorId == authorId).ToListAsync();

            return posts.Select(p => new PostModel
            {
                Id = p.Id,
                Title = p.Title,
                Content = p.Content,
                CreatedDate = p.CreatedDate,
                AuthorId = p.AuthorId
            });
        }

        public async Task<Post> AddAsync(Post post)
        {
            try
            {
                await _dbSet.AddAsync(post);
                await _context.SaveChangesAsync();
                return post;
            }
            catch (Exception ex)
            {
                throw new PostException("Error al crear el post.", ex);
            }
        }

        public async Task UpdateAsync(int id, Post post)
        {
            var existing = await _dbSet.FindAsync(id)
                ?? throw new PostException($"Post con Id {id} no encontrado.");

            existing.Title = post.Title;
            existing.Content = post.Content;
            existing.AuthorId = post.AuthorId;

            if (post.CreatedDate != default)
                existing.CreatedDate = post.CreatedDate;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new PostException("Error al actualizar el post.", ex);
            }
        }

        public async Task DeleteAsync(int id)
        {
            var post = await _dbSet.FindAsync(id)
                ?? throw new PostException($"Post con Id {id} no encontrado.");

            try
            {
                _dbSet.Remove(post);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new PostException("Error al eliminar el post.", ex);
            }
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _dbSet.AnyAsync(p => p.Id == id);
        }
    }
}
