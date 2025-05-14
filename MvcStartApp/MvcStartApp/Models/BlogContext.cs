//-
using System;

using Microsoft.EntityFrameworkCore;

using MvcStartApp.Models.Db;


namespace MvcStartApp.Models;

/// <summary>
/// Класс контекста, предоставляющий доступ к сущностям базы данных
/// </summary>
public sealed class BlogContext : DbContext
{
    /// Ссылка на таблицу Users
    public DbSet<User> Users { get; set; }
    
    /// Ссылка на таблицу UserPosts
    public DbSet<UserPost> UserPosts { get; set; }

    /// Ссылка на таблицу Requests
    public DbSet<Request> Requests { get; set; }


    // Логика взаимодействия с таблицами в БД
    public BlogContext(DbContextOptions<BlogContext> options)  : base(options)
    {
        Database.EnsureCreated();
    }

    /*
    // Назначаем связь сущности с таблицей явным образом
    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<User>().ToTable("User");
    }
    */
}
