//-
using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MvcStartApp.Models.Db;


namespace MvcStartApp.Models;

public class BlogRepository : IBlogRepository
{
    // ссылка на контекст
    private readonly BlogContext _context;

    // Метод-конструктор для инициализации
    public BlogRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task AddUser(User user)
    {
        // Добавление пользователя
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            await _context.Users.AddAsync(user);
        
        // Сохранение изенений
        await _context.SaveChangesAsync();
    }

    public async Task<User[]> GetUsers()
    {
        // Получим всех активных пользователей
        return await _context.Users.ToArrayAsync();
    }
}
