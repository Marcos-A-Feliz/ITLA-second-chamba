using Microsoft.EntityFrameworkCore;
using MyLitleProgram.Domain.Core;
using MyLitleProgram.Infrastructure.Context;

namespace MyLitleProgram.Infrastructure.Core
{
    public abstract class BaseRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _context;
        protected readonly DbSet<T> _dbSet;

        protected BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
    }
}
