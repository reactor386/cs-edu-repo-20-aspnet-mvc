//-
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

using Microsoft.EntityFrameworkCore;

using MvcStartApp.Models;
using MvcStartApp.Middlewares;
using MvcStartApp.Tools;


namespace MvcStartApp;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // получаем строку подключения из файла конфигурации
        string? connection = builder.Configuration.GetConnectionString("DefaultConnection");
        // обновляем публичные значения реальными значениями из приватной области
        connection = ConnectionTools.GetConnectionString(connection);

        // добавляем контекст ApplicationContext в качестве сервиса в приложение
        // builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection), ServiceLifetime.Singleton);

        // регистрация сервиса репозитория для взаимодействия с базой данных
        // builder.Services.AddSingleton<IBlogRepository, BlogRepository>();
        // builder.Services.AddSingleton<IRequestLogRepository, RequestLogRepository>();

        builder.Services.AddDbContext<BlogContext>(options => options.UseSqlServer(connection));

        builder.Services.AddScoped<IBlogRepository, BlogRepository>();
        builder.Services.AddScoped<IRequestLogRepository, RequestLogRepository>();

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseRouting();

        // Подключаем логирвоание с использованием ПО промежуточного слоя
        app.UseMiddleware<LoggingMiddleware>();

        app.UseAuthorization();

        app.MapStaticAssets();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}")
            .WithStaticAssets();

        app.Run();
    }
}
