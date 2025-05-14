//-
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

using MvcStartApp.Models;
using MvcStartApp.Models.Db;


namespace MvcStartApp.Middlewares;

public class LoggingMiddleware
{
    private readonly RequestDelegate _next;

    // добавляем репозиторий для взаимодействия с базой данных
    // private IBlogRepository _repo;

    /// <summary>
    ///  Middleware-компонент должен иметь конструктор, принимающий RequestDelegate
    /// </summary>
    public LoggingMiddleware(RequestDelegate next)
    {
        _next = next;
        // _repo = repo;
    }
  
    /// <summary>
    ///  Необходимо реализовать метод Invoke  или InvokeAsync
    /// </summary>
    public async Task InvokeAsync(HttpContext context)
    {
        /*
        // действия, если репозиторий подключен к данному модулю (middleware) напрямую минуя контроллер

        // string userAgent = context.Request.Headers["User-Agent"][0] ?? string.Empty;
        User user = new()
        {
            Id = Guid.NewGuid(),
            JoinDate = DateTime.Now,
            FirstName = "",
            LastName = "",
            // userAgent = userAgent
        };

        // записываем данные в базу с помощью репозитория
        await _repo.AddUser(user);
        */

        // Для логирования данных о запросе используем свойста объекта HttpContext
        Console.WriteLine($"[{DateTime.Now}]: New request to http://{(context.Request.Host.Value ?? string.Empty) + context.Request.Path}");
        
        // создаем сущность записи и объект репозитория
        Request request = new()
        {
            Id = Guid.NewGuid(),
            Date = DateTime.Now,
            Url = $"http://{(context.Request.Host.Value ?? string.Empty) + context.Request.Path}",
        };

        BlogContext dbContext = context.RequestServices.GetRequiredService<BlogContext>();
        IRequestLogRepository logRepo = new RequestLogRepository(dbContext);

        // записываем данные в базу с помощью репозитория
        await logRepo.AddRequest(request);

        // Передача запроса далее по конвейеру
        await _next.Invoke(context);
    }
}
