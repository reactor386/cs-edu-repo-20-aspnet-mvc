//-
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MvcStartApp.Models;


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
}
