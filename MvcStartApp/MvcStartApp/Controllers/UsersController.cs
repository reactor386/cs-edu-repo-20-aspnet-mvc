//-
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MvcStartApp.Models;
using MvcStartApp.Models.Db;


namespace MvcStartApp.Controllers;

public class UsersController : Controller
{
    private readonly IBlogRepository _repo;

    public UsersController(IBlogRepository repo)
    {
        _repo = repo;
    }

    public async Task<IActionResult> Index()
    {
        var authors = await _repo.GetUsers();

        // Выведем результат в консоль
        Console.WriteLine($"index in action on {authors[0]}");

        return View(authors);
    }

    /*
    public async Task<IActionResult> Register()
    {
        return View();
    }
    */


    /*
    [HttpPost]
    public async Task Register(User user)
    {
        user.JoinDate = DateTime.Now;
        user.Id = Guid.NewGuid();
        
        // Добавление пользователя
        var entry = _context.Entry(user);
        if (entry.State == EntityState.Detached)
            await _context.Users.AddAsync(user);
        
        // Сохранение изменений
        await _context.SaveChangesAsync();
        return Content($"Registration successful, {user.FirstName}");
    }
    */


    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }
    
    [HttpPost]
    public async Task<IActionResult> Register (User newUser)
    {
        newUser.JoinDate = DateTime.Now;
        // newUser.Id = Guid.NewGuid();

        await _repo.AddUser(newUser);
        return View(newUser);
    }
}
