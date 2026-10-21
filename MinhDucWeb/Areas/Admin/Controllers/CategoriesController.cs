using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;
using MinhDucWeb.Models;

namespace MinhDucWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AdminAuthorize]
    public class CategoriesController : Controller
    {
        private readonly ShopDbContext _context;

        public CategoriesController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Categories
        public async Task<IActionResult> Index()
        {
            var categories = await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();

            return View(categories);
        }

        // POST: Admin/Categories/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Category category)
        {
            if (ModelState.IsValid)
            {
                category.CreatedAt = DateTime.Now;
                _context.Categories.Add(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Thêm danh mục mới thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Categories/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                TempData["SuccessMessage"] = "Cập nhật danh mục thành công!";
            }
            return RedirectToAction(nameof(Index));
        }

        // POST: Admin/Categories/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (category != null)
            {
                if (category.Products.Any())
                {
                    TempData["ErrorMessage"] = "Không thể xóa danh mục đang có sản phẩm!";
                }
                else
                {
                    _context.Categories.Remove(category);
                    await _context.SaveChangesAsync();
                    TempData["SuccessMessage"] = "Xóa danh mục thành công!";
                }
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
