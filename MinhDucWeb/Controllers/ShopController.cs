using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;
using MinhDucWeb.Models;

namespace MinhDucWeb.Controllers
{
    public class ShopController : Controller
    {
        private readonly ShopDbContext _context;

        public ShopController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: /Shop
        public async Task<IActionResult> Index(string? search, int? categoryId)
        {
            var query = _context.Products.Include(p => p.Category).AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                var keyword = search.Trim().ToLower();
                query = query.Where(p => p.Name.ToLower().Contains(keyword) || p.Brand.ToLower().Contains(keyword));
                ViewBag.CurrentSearch = search;
            }

            if (categoryId.HasValue && categoryId.Value > 0)
            {
                query = query.Where(p => p.CategoryId == categoryId.Value);
                ViewBag.CurrentCategoryId = categoryId.Value;
            }

            var products = await query.OrderByDescending(p => p.Id).ToListAsync();
            ViewBag.Categories = await _context.Categories.ToListAsync();

            return View(products);
        }

        // GET: /Shop/Detail/5
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            // Lấy 4 sản phẩm cùng danh mục liên quan
            var relatedProducts = await _context.Products
                .Where(p => p.CategoryId == product.CategoryId && p.Id != product.Id)
                .Take(4)
                .ToListAsync();

            if (!relatedProducts.Any())
            {
                relatedProducts = await _context.Products
                    .Where(p => p.Id != product.Id)
                    .Take(4)
                    .ToListAsync();
            }

            ViewBag.RelatedProducts = relatedProducts;

            return View(product);
        }
    }
}
