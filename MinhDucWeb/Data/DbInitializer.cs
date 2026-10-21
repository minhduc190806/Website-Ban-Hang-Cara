using System;
using System.Linq;
using MinhDucWeb.Models;

namespace MinhDucWeb.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ShopDbContext context)
        {
            // Tự động tạo Database và cấu trúc bảng nếu chưa tồn tại
            context.Database.EnsureCreated();

            // Nếu đã có sản phẩm thì không nạp lại
            if (context.Products.Any())
            {
                return;
            }

            // 1. Nạp Danh mục (Categories)
            var cat1 = new Category { Name = "Áo Thun & Sơ Mi Hè", Description = "Các mẫu áo phông, áo sơ mi thoáng mát phong cách trẻ trung" };
            var cat2 = new Category { Name = "Bộ Sưu Tập Nổi Bật", Description = "Các sản phẩm hot bán chạy nhất mùa hè" };
            var cat3 = new Category { Name = "Hàng Mới Về 2026", Description = "Mẫu thiết kế thời trang hiện đại mới cập bến" };
            var cat4 = new Category { Name = "Thời Trang Nữ & Phụ Kiện", Description = "Trang phục và phụ kiện cao cấp thanh lịch" };

            context.Categories.AddRange(cat1, cat2, cat3, cat4);
            context.SaveChanges();

            // 2. Nạp Sản phẩm (Products)
            var products = new Product[]
            {
                new Product { CategoryId = cat1.Id, Name = "Cartoon Astronaut T-Shirts V1", Brand = "Adidas", Price = 100.00m, OldPrice = 140.00m, ImageUrl = "img/products/f1.jpg", Description = "Áo thun cotton cao cấp in hình phi hành gia cartoon sắc nét, thoáng khí 4 chiều.", Stock = 50, IsFeatured = true },
                new Product { CategoryId = cat1.Id, Name = "Cartoon Astronaut T-Shirts V2", Brand = "Adidas", Price = 110.00m, OldPrice = 150.00m, ImageUrl = "img/products/f2.jpg", Description = "Áo thun họa tiết cây hoa nhiệt đới mùa hè rực rỡ, mềm mại thoải mái vận động.", Stock = 45, IsFeatured = true },
                new Product { CategoryId = cat1.Id, Name = "Cartoon Astronaut T-Shirts V3", Brand = "Adidas", Price = 105.00m, OldPrice = 135.00m, ImageUrl = "img/products/f3.jpg", Description = "Thiết kế áo phong cách vintage retro, chất vải cotton 100% thấm hút mồ hôi tối đa.", Stock = 60, IsFeatured = true },
                new Product { CategoryId = cat1.Id, Name = "Cartoon Astronaut T-Shirts V4", Brand = "Adidas", Price = 95.00m, OldPrice = 120.00m, ImageUrl = "img/products/f4.jpg", Description = "Áo phông họa tiết phong cách đường phố cá tính, form rộng streetwear.", Stock = 30, IsFeatured = true },
                new Product { CategoryId = cat2.Id, Name = "Cartoon Astronaut T-Shirts V5", Brand = "Adidas", Price = 125.00m, OldPrice = 160.00m, ImageUrl = "img/products/f5.jpg", Description = "Sơ mi hoa lá mùa hè hawaii nổi bật, nhẹ nhàng bay bổng.", Stock = 40, IsFeatured = true },
                new Product { CategoryId = cat2.Id, Name = "Cartoon Astronaut T-Shirts V6", Brand = "Adidas", Price = 130.00m, OldPrice = 170.00m, ImageUrl = "img/products/f6.jpg", Description = "Áo khoác sơ mi dài tay phối hai màu cá tính, đường may chắc chắn.", Stock = 25, IsFeatured = true },
                new Product { CategoryId = cat2.Id, Name = "Cartoon Astronaut T-Shirts V7", Brand = "Adidas", Price = 85.00m, OldPrice = 115.00m, ImageUrl = "img/products/f7.jpg", Description = "Quần short ống rộng thoải mái đi biển, túi khóa tiện lợi.", Stock = 70, IsFeatured = true },
                new Product { CategoryId = cat2.Id, Name = "Cartoon Astronaut T-Shirts V8", Brand = "Adidas", Price = 90.00m, OldPrice = 120.00m, ImageUrl = "img/products/f8.jpg", Description = "Áo blouse nữ họa tiết thời trang thanh lịch, nhẹ nhàng.", Stock = 35, IsFeatured = true },

                new Product { CategoryId = cat3.Id, Name = "Cartoon Astronaut Shirt N1", Brand = "Zara", Price = 115.00m, OldPrice = 150.00m, ImageUrl = "img/products/n1.jpg", Description = "Sơ mi công sở vải Oxford cao cấp chống nhăn, tôn dáng thanh lịch.", Stock = 40, IsNewArrival = true },
                new Product { CategoryId = cat3.Id, Name = "Cartoon Astronaut Shirt N2", Brand = "Zara", Price = 120.00m, OldPrice = 160.00m, ImageUrl = "img/products/n2.jpg", Description = "Sơ mi caro xám trẻ trung, dễ phối đồ với quần jeans hoặc kaki.", Stock = 55, IsNewArrival = true },
                new Product { CategoryId = cat3.Id, Name = "Cartoon Astronaut Shirt N3", Brand = "Zara", Price = 99.00m, OldPrice = 130.00m, ImageUrl = "img/products/n3.jpg", Description = "Sơ mi trắng basic tối giản tinh tế, cổ áo đứng form chuẩn.", Stock = 80, IsNewArrival = true },
                new Product { CategoryId = cat3.Id, Name = "Cartoon Astronaut Shirt N4", Brand = "Nike", Price = 108.00m, OldPrice = 145.00m, ImageUrl = "img/products/n4.jpg", Description = "Áo polo thể thao chất vải cá sấu gai mịn, thấm hút mồ hôi nhanh.", Stock = 65, IsNewArrival = true },
                new Product { CategoryId = cat4.Id, Name = "Cartoon Astronaut Shirt N5", Brand = "Nike", Price = 112.00m, OldPrice = 140.00m, ImageUrl = "img/products/n5.jpg", Description = "Sơ mi denim xanh cá tính bền bỉ, phối wash nhẹ hiện đại.", Stock = 35, IsNewArrival = true },
                new Product { CategoryId = cat4.Id, Name = "Cartoon Astronaut Shirt N6", Brand = "Gucci", Price = 75.00m, OldPrice = 95.00m, ImageUrl = "img/products/n6.jpg", Description = "Quần soóc kaki co giãn nhẹ, phối túi tiện lợi phong cách năng động.", Stock = 50, IsNewArrival = true },
                new Product { CategoryId = cat4.Id, Name = "Cartoon Astronaut Shirt N7", Brand = "Gucci", Price = 129.00m, OldPrice = 175.00m, ImageUrl = "img/products/n7.jpg", Description = "Sơ mi vàng cát họa tiết bohemieng phá cách độc đáo.", Stock = 20, IsNewArrival = true },
                new Product { CategoryId = cat4.Id, Name = "Cartoon Astronaut Shirt N8", Brand = "Adidas", Price = 118.00m, OldPrice = 155.00m, ImageUrl = "img/products/n8.jpg", Description = "Áo sơ mi tay ngắn tone đen sang trọng, vải lụa tơ tằm siêu mềm.", Stock = 45, IsNewArrival = true }
            };

            context.Products.AddRange(products);
            context.SaveChanges();

            // 3. Nạp Khách hàng (Customers)
            var c1 = new Customer { FullName = "Nguyễn Minh Đức", Email = "minhduc@gmail.com", PhoneNumber = "0912345678", Address = "Số 123 Đường Cầu Giấy, Hà Nội" };
            var c2 = new Customer { FullName = "Trần Thị Mai", Email = "maitran@gmail.com", PhoneNumber = "0987654321", Address = "Số 45 Lê Lợi, Quận 1, TP. Hồ Chí Minh" };
            var c3 = new Customer { FullName = "Lê Hoàng Long", Email = "longle@gmail.com", PhoneNumber = "0905123987", Address = "Số 88 Nguyễn Văn Linh, Đà Nẵng" };

            context.Customers.AddRange(c1, c2, c3);
            context.SaveChanges();

            // 4. Nạp Hóa đơn (Orders)
            var order1 = new Order
            {
                OrderCode = "MD-20261020-001",
                CustomerId = c1.Id,
                CustomerName = c1.FullName,
                CustomerPhone = c1.PhoneNumber,
                CustomerEmail = c1.Email,
                ShippingAddress = c1.Address,
                OrderNotes = "Giao giờ hành chính",
                PaymentMethod = "COD (Thanh toán khi nhận hàng)",
                TotalAmount = 210.00m,
                Status = "Đã hoàn thành",
                OrderDate = DateTime.Now.AddDays(-2)
            };

            var order2 = new Order
            {
                OrderCode = "MD-20261020-002",
                CustomerId = c2.Id,
                CustomerName = c2.FullName,
                CustomerPhone = c2.PhoneNumber,
                CustomerEmail = c2.Email,
                ShippingAddress = c2.Address,
                OrderNotes = "Gọi trước khi giao",
                PaymentMethod = "Chuyển khoản ngân hàng",
                TotalAmount = 194.00m,
                Status = "Đang giao",
                OrderDate = DateTime.Now.AddDays(-1)
            };

            var order3 = new Order
            {
                OrderCode = "MD-20261020-003",
                CustomerId = c3.Id,
                CustomerName = c3.FullName,
                CustomerPhone = c3.PhoneNumber,
                CustomerEmail = c3.Email,
                ShippingAddress = c3.Address,
                OrderNotes = "Đóng gói cẩn thận",
                PaymentMethod = "COD (Thanh toán khi nhận hàng)",
                TotalAmount = 100.00m,
                Status = "Chờ xử lý",
                OrderDate = DateTime.Now
            };

            context.Orders.AddRange(order1, order2, order3);
            context.SaveChanges();

            // 5. Nạp Chi tiết Hóa đơn (OrderDetails)
            var details = new OrderDetail[]
            {
                new OrderDetail { OrderId = order1.Id, ProductId = products[0].Id, ProductName = products[0].Name, ProductImage = products[0].ImageUrl, Size = "L", Price = 100.00m, Quantity = 1, SubTotal = 100.00m },
                new OrderDetail { OrderId = order1.Id, ProductId = products[1].Id, ProductName = products[1].Name, ProductImage = products[1].ImageUrl, Size = "XL", Price = 110.00m, Quantity = 1, SubTotal = 110.00m },
                new OrderDetail { OrderId = order2.Id, ProductId = products[3].Id, ProductName = products[3].Name, ProductImage = products[3].ImageUrl, Size = "M", Price = 95.00m, Quantity = 1, SubTotal = 95.00m },
                new OrderDetail { OrderId = order2.Id, ProductId = products[10].Id, ProductName = products[10].Name, ProductImage = products[10].ImageUrl, Size = "L", Price = 99.00m, Quantity = 1, SubTotal = 99.00m },
                new OrderDetail { OrderId = order3.Id, ProductId = products[0].Id, ProductName = products[0].Name, ProductImage = products[0].ImageUrl, Size = "M", Price = 100.00m, Quantity = 1, SubTotal = 100.00m }
            };

            context.OrderDetails.AddRange(details);

            // 6. Nạp Tài khoản Quản trị Admin
            var admin = new AdminUser
            {
                Username = "admin",
                Password = "admin123",
                FullName = "Quản Trị Viên Minh Đức",
                Role = "Admin"
            };
            context.Admins.Add(admin);

            context.SaveChanges();
        }
    }
}
