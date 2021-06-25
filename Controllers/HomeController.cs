using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using aspnetcore_redis_cache.Models;
using aspnetcore_redis_cache.Data;
using Microsoft.Extensions.Caching.Distributed;

namespace aspnetcore_redis_cache.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IDistributedCache _cache;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, IDistributedCache cache)
        {
            _logger = logger;
            _context = context;
            _cache = cache;
        }

        public async Task<IActionResult> Generate()
        {
            for (int i = 0; i < 100000; i++)
            {
                var newBook = new Book()
                {
                    Price = 10m,
                    Title = "My book!",
                    Publisher = new Publisher()
                    {
                        Name = "Utharn Printing"
                    }
                };
                await _context.Books.AddAsync(newBook);
            }
            await _context.SaveChangesAsync();
            return Ok("generate");
        }

        public async Task<IActionResult> Retrieve(int id)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            var book = await _cache.GetObjectAsync<Book>("book:" + id);
            if (book == null)
            {
                book = await _context.Books.FindAsync(id);
                await _cache.SetObjectAsync("book:" + book.BookId, book);
            }
            stopwatch.Stop();
            // return Ok(book);
            return Ok(stopwatch.ElapsedMilliseconds);
        }

        public async Task<IActionResult> Remove(int id)
        {
            await _cache.RemoveAsync("book:" + id);
            return Content("removed: " + id);
        }
    }
}
