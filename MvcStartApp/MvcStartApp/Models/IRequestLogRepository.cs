//-
using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using MvcStartApp.Models.Db;


namespace MvcStartApp.Models;

public interface IRequestLogRepository
{
    Task AddRequest(Request request);
    
    Task<Request[]> GetRequests();
}
