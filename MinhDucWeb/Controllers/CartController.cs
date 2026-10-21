using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;
using MinhDucWeb.Models;

namespace MinhDucWeb.Controllers
{
    public class CartController : Controller
    {
        private readonly ShopDbContext _context;
        private const string CartSessionKey = "SHOP_CART_SESSION";

        public CartController(ShopDbContext context)
        {
            _context = context;
        }

        private List<CartItem> GetCart()
        {
            var json = HttpContext.Session.GetString(CartSessionKey);
            if (string.IsNullOrEmpty(json))
            {
                return new List<CartItem>();
            }
            return JsonSerializer.Deserialize<List<CartItem>>(json) ?? new List<CartItem>();
        }

        private void SaveCart(List<CartItem> cart)
        {
            var json = JsonSerializer.Serialize(cart);
            HttpContext.Session.SetString(CartSessionKey, json);
        }

        // GET: /Cart
        public IActionResult Index()
        {
            var cart = GetCart();
            return View(cart);
        }

        // POST: /Cart/AddToCart
        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity = 1, string size = "L")
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null)
            {
                return NotFound();
            }

            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId && i.Size == size);

            if (item != null)
            {
                item.Quantity += quantity;
            }
            else
            {
                cart.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Brand = product.Brand,
                    Price = product.Price,
                    ImageUrl = product.ImageUrl,
                    Size = size,
                    Quantity = quantity
                });
            }

            SaveCart(cart);

            // Nếu gọi qua Ajax (Fetch)
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                return Json(new { success = true, totalCount = cart.Sum(i => i.Quantity), message = "Thêm vào giỏ thành công!" });
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/UpdateQuantity
        [HttpPost]
        public IActionResult UpdateQuantity(int productId, string size, int quantity)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId && i.Size == size);

            if (item != null)
            {
                if (quantity <= 0)
                {
                    cart.Remove(item);
                }
                else
                {
                    item.Quantity = quantity;
                }
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Remove
        [HttpPost]
        public IActionResult Remove(int productId, string size)
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(i => i.ProductId == productId && i.Size == size);

            if (item != null)
            {
                cart.Remove(item);
                SaveCart(cart);
            }

            return RedirectToAction(nameof(Index));
        }

        // POST: /Cart/Clear
        [HttpPost]
        public IActionResult Clear()
        {
            HttpContext.Session.Remove(CartSessionKey);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Cart/Checkout
        public IActionResult Checkout()
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            return View(cart);
        }

        // POST: /Cart/ProcessCheckout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ProcessCheckout(string fullName, string phone, string email, string address, string? notes, string paymentMethod)
        {
            var cart = GetCart();
            if (!cart.Any())
            {
                return RedirectToAction(nameof(Index));
            }

            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(phone) || string.IsNullOrWhiteSpace(address))
            {
                ModelState.AddModelError("", "Vui lòng nhập đầy đủ họ tên, số điện thoại và địa chỉ nhận hàng.");
                return View("Checkout", cart);
            }

            // 1. Kiểm tra hoặc tạo mới Khách Hàng (Customer)
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.PhoneNumber == phone.Trim());
            if (customer == null)
            {
                customer = new Customer
                {
                    FullName = fullName.Trim(),
                    PhoneNumber = phone.Trim(),
                    Email = email?.Trim() ?? $"{phone}@shop.com",
                    Address = address.Trim(),
                    CreatedAt = DateTime.Now
                };
                _context.Customers.Add(customer);
                await _context.SaveChangesAsync();
            }

            // 2. Tạo Hóa Đơn (Order)
            var orderCode = "MD-" + DateTime.Now.ToString("yyyyMMdd") + "-" + new Random().Next(100, 999);
            var totalAmount = cart.Sum(i => i.Total);

            var order = new Order
            {
                OrderCode = orderCode,
                CustomerId = customer.Id,
                CustomerName = fullName.Trim(),
                CustomerPhone = phone.Trim(),
                CustomerEmail = email?.Trim(),
                ShippingAddress = address.Trim(),
                OrderNotes = notes?.Trim(),
                PaymentMethod = paymentMethod ?? "COD (Thanh toán khi nhận hàng)",
                TotalAmount = totalAmount,
                Status = "Chờ xử lý",
                OrderDate = DateTime.Now
            };

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // 3. Tạo Chi Tiết Hóa Đơn (OrderDetails)
            foreach (var item in cart)
            {
                var detail = new OrderDetail
                {
                    OrderId = order.Id,
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    ProductImage = item.ImageUrl,
                    Size = item.Size,
                    Price = item.Price,
                    Quantity = item.Quantity,
                    SubTotal = item.Total
                };
                _context.OrderDetails.Add(detail);
            }

            await _context.SaveChangesAsync();

            // 4. Xóa rỗng giỏ hàng sau khi đặt thành công
            HttpContext.Session.Remove(CartSessionKey);

            return RedirectToAction(nameof(Success), new { code = order.OrderCode, name = order.CustomerName, total = order.TotalAmount });
        }

        // GET: /Cart/Success
        public IActionResult Success(string code, string name, decimal total)
        {
            ViewBag.OrderCode = code;
            ViewBag.CustomerName = name;
            ViewBag.TotalAmount = total;
            return View();
        }
    }
}
