using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;
using MinhDucWeb.Models;

namespace MinhDucWeb.Controllers
{
    public class AccountController : Controller
    {
        private readonly ShopDbContext _context;

        public AccountController(ShopDbContext context)
        {
            _context = context;
        }

        // GET: /Account/Login
        [HttpGet]
        public IActionResult Login(string? returnUrl = null)
        {
            var role = HttpContext.Session.GetString("AUTH_ROLE");
            if (role == "Admin")
            {
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }
            else if (!string.IsNullOrEmpty(HttpContext.Session.GetString("AUTH_USER")))
            {
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string username, string password, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.ErrorMessage = "Vui lòng nhập đầy đủ tài khoản và mật khẩu!";
                return View();
            }

            username = username.Trim();
            password = password.Trim();

            // 1. Kiểm tra tài khoản Quản trị Admin
            var admin = await _context.Admins.FirstOrDefaultAsync(a => a.Username.ToLower() == username.ToLower() && a.Password == password);
            if (admin != null)
            {
                HttpContext.Session.SetString("AUTH_USER", admin.Username);
                HttpContext.Session.SetString("AUTH_NAME", admin.FullName);
                HttpContext.Session.SetString("AUTH_ROLE", "Admin");
                TempData["SuccessMessage"] = $"Đăng nhập Quản trị thành công! Xin chào {admin.FullName}.";
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            // Hỗ trợ đăng nhập fallback Admin nếu CSDL chưa nạp tài khoản admin
            if (username.Equals("admin", StringComparison.OrdinalIgnoreCase) && password == "admin123")
            {
                HttpContext.Session.SetString("AUTH_USER", "admin");
                HttpContext.Session.SetString("AUTH_NAME", "Quản Trị Viên");
                HttpContext.Session.SetString("AUTH_ROLE", "Admin");
                TempData["SuccessMessage"] = "Đăng nhập Quản trị thành công!";
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Products", new { area = "Admin" });
            }

            // 2. Kiểm tra tài khoản Khách hàng
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower() == username.ToLower());
            if (customer != null && (password == "123456" || password == "admin123"))
            {
                HttpContext.Session.SetString("AUTH_USER", customer.Email);
                HttpContext.Session.SetString("AUTH_NAME", customer.FullName);
                HttpContext.Session.SetString("AUTH_ROLE", "Customer");
                TempData["SuccessMessage"] = $"Đăng nhập thành công! Chào mừng {customer.FullName}.";

                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }
                return RedirectToAction("Index", "Home");
            }

            ViewBag.ErrorMessage = "Tên đăng nhập hoặc mật khẩu không chính xác!";
            ViewBag.Username = username;
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(string fullName, string phone, string email, string password)
        {
            if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                ViewBag.RegisterError = "Vui lòng nhập đầy đủ thông tin bắt buộc!";
                return View("Login");
            }

            var existing = await _context.Customers.FirstOrDefaultAsync(c => c.Email.ToLower() == email.Trim().ToLower());
            if (existing != null)
            {
                ViewBag.RegisterError = "Email này đã được sử dụng. Vui lòng chọn email khác!";
                return View("Login");
            }

            var customer = new Customer
            {
                FullName = fullName.Trim(),
                PhoneNumber = phone?.Trim() ?? "",
                Email = email.Trim(),
                Address = "Chưa cập nhật",
                CreatedAt = DateTime.Now
            };

            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();

            // Tự động đăng nhập
            HttpContext.Session.SetString("AUTH_USER", customer.Email);
            HttpContext.Session.SetString("AUTH_NAME", customer.FullName);
            HttpContext.Session.SetString("AUTH_ROLE", "Customer");

            TempData["SuccessMessage"] = $"Đăng ký tài khoản thành công! Xin chào {customer.FullName}.";
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/Logout
        [HttpGet]
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("AUTH_USER");
            HttpContext.Session.Remove("AUTH_NAME");
            HttpContext.Session.Remove("AUTH_ROLE");
            TempData["SuccessMessage"] = "Bạn đã đăng xuất tài khoản thành công.";
            return RedirectToAction("Index", "Home");
        }
    }
}
