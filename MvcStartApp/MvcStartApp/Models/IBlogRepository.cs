//-
using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MvcStartApp.Models.Db;


namespace MvcStartApp.Models;

public interface IBlogRepository
{
    Task AddUser(User user);
    
    Task<User[]> GetUsers();
}
