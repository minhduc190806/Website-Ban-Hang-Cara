using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;
using MinhDucWeb.Models;

namespace MinhDucWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ShopDbContext _context;

        public HomeController(ShopDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var featuredProducts = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsFeatured)
                .Take(8)
                .ToListAsync();

            var newArrivals = await _context.Products
                .Include(p => p.Category)
                .Where(p => p.IsNewArrival)
                .Take(8)
                .ToListAsync();

            ViewBag.FeaturedProducts = featuredProducts;
            ViewBag.NewArrivals = newArrivals;

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Blog()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        // Tải xuống file CSDL SQL Script chuẩn
        [HttpGet("/ShopMinhDucDB.sql")]
        public IActionResult DownloadSql()
        {
            var wwwrootSql = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "ShopMinhDucDB.sql");
            if (System.IO.File.Exists(wwwrootSql))
            {
                return PhysicalFile(Path.GetFullPath(wwwrootSql), "application/sql", "ShopMinhDucDB.sql");
            }

            var rootSql = Path.Combine(Directory.GetCurrentDirectory(), "..", "ShopMinhDucDB.sql");
            if (System.IO.File.Exists(rootSql))
            {
                return PhysicalFile(Path.GetFullPath(rootSql), "application/sql", "ShopMinhDucDB.sql");
            }

            return NotFound("Không tìm thấy tệp ShopMinhDucDB.sql.");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View();
        }
    }
}
