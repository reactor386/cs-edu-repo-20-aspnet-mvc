//-
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using MvcStartApp.Models;


namespace MvcStartApp.Controllers;

public class LogsController : Controller
{
    private readonly IRequestLogRepository _repo;
    
    public LogsController(IRequestLogRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<IActionResult> Index()
    {
        var requests = await _repo.GetRequests();

        // Выведем результат в консоль
        Console.WriteLine($"index in action on {requests[0]}");

        return View(requests.ToList());
    }
}
