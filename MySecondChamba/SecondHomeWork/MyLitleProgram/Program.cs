using Microsoft.EntityFrameworkCore;
using MyLittleProgram.Data;
using MyLittleProgram.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=(localdb)\\mssqllocaldb;Database=MyLittleProgramDB;Trusted_Connection=True;"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

    db.Database.EnsureCreated();

    if (!db.Authors.Any())
    {
        var author = new Author { Name = "Admin", Email = "admin@blog.com" };
        db.Authors.Add(author);
        db.SaveChanges();

        db.Posts.Add(new Post
        {
            Title = "Primer Post",
            Content = "Bienvenido al blog",
            AuthorId = author.Id
        });
        db.SaveChanges();
    }
}

app.Run();