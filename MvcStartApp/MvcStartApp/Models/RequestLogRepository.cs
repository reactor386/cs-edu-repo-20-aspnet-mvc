//-
using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MvcStartApp.Models.Db;


namespace MvcStartApp.Models;

public class RequestLogRepository : IRequestLogRepository
{
    // ссылка на контекст
    private readonly BlogContext _context;

    // Метод-конструктор для инициализации
    public RequestLogRepository(BlogContext context)
    {
        _context = context;
    }

    public async Task AddRequest(Request request)
    {
        // Добавление пользователя
        var entry = _context.Entry(request);
        if (entry.State == EntityState.Detached)
            await _context.Requests.AddAsync(request);
        
        // Сохранение изенений
        await _context.SaveChangesAsync();
    }

    public async Task<Request[]> GetRequests()
    {
        // Получим все записи
        return await _context.Requests.ToArrayAsync();
    }
}
