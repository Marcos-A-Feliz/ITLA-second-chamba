using Microsoft.EntityFrameworkCore;
using MyLitleProgram.Application.Contract;
using MyLitleProgram.Application.Service;
using MyLitleProgram.Domain.Entities;
using MyLitleProgram.Infrastructure.Context;
using MyLitleProgram.Infrastructure.Interfaces;
using MyLitleProgram.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Base de datos
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")
        ?? "Server=(localdb)\\mssqllocaldb;Database=MyLittleProgramDB;Trusted_Connection=True;"));

// Repositorios (Infrastructure)
builder.Services.AddScoped<IAuthorRepository, AuthorRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();

// Servicios (Application)
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IPostService, PostService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Seed de datos iniciales
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
