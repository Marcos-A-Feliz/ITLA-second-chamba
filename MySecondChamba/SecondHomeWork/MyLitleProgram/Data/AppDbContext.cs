using Microsoft.EntityFrameworkCore;
using MyLittleProgram.Models;

namespace MyLittleProgram.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Author> Authors { get; set; }
        public DbSet<Post> Posts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Post>()
                .HasOne(p => p.Author)           
                .WithMany(a => a.Posts)          
                .HasForeignKey(p => p.AuthorId)  
                .OnDelete(DeleteBehavior.Cascade); 
        }
    }
}
