//-
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using MvcStartApp.Models;
using MvcStartApp.Models.Db;


namespace MvcStartApp.Controllers;

public class FeedbackController : Controller
{
   /// <summary>
   ///  Метод, возвращающий страницу с отзывами
   /// </summary>
   [HttpGet]
   public IActionResult Add()
   {
       return View();
   }
  
   /// <summary>
   /// Метод для Ajax-запросов
   /// </summary>
   [HttpPost]
   public IActionResult Add(Feedback feedback)
   {
       return StatusCode(200, $"{feedback.From}, спасибо за отзыв!");
   }
 
   [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
   public IActionResult Error()
   {
       return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
   }
}
