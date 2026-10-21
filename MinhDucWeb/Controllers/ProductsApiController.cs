using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;
using MinhDucWeb.Models;

namespace MinhDucWeb.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductsApiController : ControllerBase
    {
        private readonly ShopDbContext _context;

        public ProductsApiController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: api/products (Lấy danh sách sản phẩm từ CSDL)
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .OrderByDescending(p => p.Id)
                .ToListAsync();

            var result = products.Select(p => new
            {
                id = p.Id,
                name = p.Name,
                category = p.Category != null ? p.Category.Name : "Áo Thun & Sơ Mi Hè",
                categoryId = p.CategoryId,
                brand = p.Brand,
                price = p.Price,
                oldPrice = p.OldPrice,
                stock = p.Stock,
                image = p.ImageUrl,
                description = p.Description,
                isFeatured = p.IsFeatured,
                isNewArrival = p.IsNewArrival
            });

            return Ok(result);
        }

        // GET: api/products/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var p = await _context.Products.Include(prod => prod.Category).FirstOrDefaultAsync(prod => prod.Id == id);
            if (p == null) return NotFound(new { success = false, message = "Không tìm thấy sản phẩm" });

            return Ok(new
            {
                id = p.Id,
                name = p.Name,
                category = p.Category != null ? p.Category.Name : "Áo Thun & Sơ Mi Hè",
                categoryId = p.CategoryId,
                brand = p.Brand,
                price = p.Price,
                oldPrice = p.OldPrice,
                stock = p.Stock,
                image = p.ImageUrl,
                description = p.Description,
                isFeatured = p.IsFeatured,
                isNewArrival = p.IsNewArrival
            });
        }

        // POST: api/products (THÊM MỚI SẢN PHẨM VÀO CSDL)
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductApiDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return BadRequest(new { success = false, message = "Tên sản phẩm không được để trống" });
            }

            // Tìm hoặc gán danh mục
            var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == dto.Category);
            if (category == null)
            {
                category = await _context.Categories.FirstOrDefaultAsync() ?? new Category { Name = "Áo Thun & Sơ Mi Hè" };
            }

            var product = new Product
            {
                CategoryId = category.Id,
                Name = dto.Name.Trim(),
                Brand = string.IsNullOrWhiteSpace(dto.Brand) ? "Adidas" : dto.Brand.Trim(),
                Price = dto.Price > 0 ? dto.Price : 100,
                OldPrice = dto.OldPrice,
                Stock = dto.Stock > 0 ? dto.Stock : 50,
                ImageUrl = string.IsNullOrWhiteSpace(dto.Image) ? "img/products/f1.jpg" : dto.Image.Trim(),
                Description = dto.Description,
                IsFeatured = dto.Category != "Hàng Mới Về 2026",
                IsNewArrival = dto.Category == "Hàng Mới Về 2026",
                CreatedAt = DateTime.Now
            };

            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                id = product.Id,
                message = $"Đã thêm mới sản phẩm '{product.Name}' vào Cơ Sở Dữ Liệu thành công!",
                product = new
                {
                    id = product.Id,
                    name = product.Name,
                    category = category.Name,
                    brand = product.Brand,
                    price = product.Price,
                    oldPrice = product.OldPrice,
                    stock = product.Stock,
                    image = product.ImageUrl,
                    description = product.Description,
                    isFeatured = product.IsFeatured,
                    isNewArrival = product.IsNewArrival
                }
            });
        }

        // PUT: api/products/5 (CHỈNH SỬA SẢN PHẨM TRONG CSDL)
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ProductApiDto dto)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { success = false, message = $"Không tìm thấy sản phẩm #{id} trong CSDL" });
            }

            if (!string.IsNullOrWhiteSpace(dto.Category))
            {
                var category = await _context.Categories.FirstOrDefaultAsync(c => c.Name == dto.Category);
                if (category != null)
                {
                    product.CategoryId = category.Id;
                }
            }

            product.Name = string.IsNullOrWhiteSpace(dto.Name) ? product.Name : dto.Name.Trim();
            if (!string.IsNullOrWhiteSpace(dto.Brand)) product.Brand = dto.Brand.Trim();
            if (dto.Price > 0) product.Price = dto.Price;
            product.OldPrice = dto.OldPrice;
            if (dto.Stock > 0) product.Stock = dto.Stock;
            if (!string.IsNullOrWhiteSpace(dto.Image)) product.ImageUrl = dto.Image.Trim();
            if (dto.Description != null) product.Description = dto.Description;
            product.IsNewArrival = dto.Category == "Hàng Mới Về 2026";
            product.IsFeatured = dto.Category != "Hàng Mới Về 2026";

            _context.Products.Update(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = $"Đã cập nhật sản phẩm #{id} vào Cơ Sở Dữ Liệu thành công!"
            });
        }

        // DELETE: api/products/5 (XÓA SẢN PHẨM KHỎI CSDL)
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new { success = false, message = $"Không tìm thấy sản phẩm #{id} trong CSDL" });
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                success = true,
                message = $"Đã xóa sản phẩm #{id} khỏi Cơ Sở Dữ Liệu thành công!"
            });
        }
    }

    public class ProductApiDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Category { get; set; }
        public string? Brand { get; set; }
        public decimal Price { get; set; }
        public decimal? OldPrice { get; set; }
        public int Stock { get; set; } = 50;
        public string? Image { get; set; }
        public string? Description { get; set; }
    }
}
