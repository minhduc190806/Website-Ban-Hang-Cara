-- ==========================================================
-- ĐỒ ÁN WEBSITE BÁN HÀNG THỜI TRANG MINHDUC TH29.25
-- HỆ THỐNG CƠ SỞ DỮ LIỆU BÁN HÀNG (SQL SERVER)
-- ==========================================================

-- 1. TẠO CƠ SỞ DỮ LIỆU
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ShopMinhDucDB')
BEGIN
    CREATE DATABASE ShopMinhDucDB;
END
GO

USE ShopMinhDucDB;
GO

-- 2. XÓA CÁC BẢNG CŨ (NẾU ĐÃ TỒN TẠI ĐỂ TẠO LẠI)
IF OBJECT_ID('OrderDetails', 'U') IS NOT NULL DROP TABLE OrderDetails;
IF OBJECT_ID('Orders', 'U') IS NOT NULL DROP TABLE Orders;
IF OBJECT_ID('Products', 'U') IS NOT NULL DROP TABLE Products;
IF OBJECT_ID('Categories', 'U') IS NOT NULL DROP TABLE Categories;
IF OBJECT_ID('Customers', 'U') IS NOT NULL DROP TABLE Customers;
IF OBJECT_ID('Admins', 'U') IS NOT NULL DROP TABLE Admins;
GO

-- 3. TẠO BẢNG 1: CATEGORIES (DANH MỤC SẢN PHẨM)
CREATE TABLE Categories (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(100) NOT NULL,
    Description NVARCHAR(255) NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 4. TẠO BẢNG 2: PRODUCTS (SẢN PHẨM)
CREATE TABLE Products (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    CategoryId INT NOT NULL,
    Name NVARCHAR(200) NOT NULL,
    Brand NVARCHAR(100) NOT NULL DEFAULT 'Adidas',
    Price DECIMAL(18,2) NOT NULL,
    OldPrice DECIMAL(18,2) NULL,
    ImageUrl NVARCHAR(255) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    Stock INT NOT NULL DEFAULT 100,
    IsFeatured BIT NOT NULL DEFAULT 0,
    IsNewArrival BIT NOT NULL DEFAULT 0,
    CreatedAt DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Products_Categories FOREIGN KEY (CategoryId) REFERENCES Categories(Id) ON DELETE CASCADE
);
GO

-- 5. TẠO BẢNG 3: CUSTOMERS (KHÁCH HÀNG)
CREATE TABLE Customers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FullName NVARCHAR(150) NOT NULL,
    Email NVARCHAR(150) NOT NULL,
    PhoneNumber NVARCHAR(20) NOT NULL,
    Address NVARCHAR(255) NOT NULL,
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- 6. TẠO BẢNG 4: ORDERS (HÓA ĐƠN / ĐƠN HÀNG)
CREATE TABLE Orders (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderCode NVARCHAR(50) NOT NULL UNIQUE,
    CustomerId INT NULL,
    CustomerName NVARCHAR(150) NOT NULL,
    CustomerPhone NVARCHAR(20) NOT NULL,
    CustomerEmail NVARCHAR(150) NULL,
    ShippingAddress NVARCHAR(255) NOT NULL,
    OrderNotes NVARCHAR(500) NULL,
    PaymentMethod NVARCHAR(50) NOT NULL DEFAULT N'COD (Thanh toán khi nhận hàng)',
    TotalAmount DECIMAL(18,2) NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT N'Chờ xử lý', 
    -- Các trạng thái: N'Chờ xử lý', N'Đã xác nhận', N'Đang giao', N'Đã hoàn thành', N'Đã hủy'
    OrderDate DATETIME DEFAULT GETDATE(),
    CONSTRAINT FK_Orders_Customers FOREIGN KEY (CustomerId) REFERENCES Customers(Id) ON DELETE SET NULL
);
GO

-- 7. TẠO BẢNG 5: ORDER_DETAILS (CHI TIẾT HÓA ĐƠN)
CREATE TABLE OrderDetails (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    OrderId INT NOT NULL,
    ProductId INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    ProductImage NVARCHAR(255) NULL,
    Size NVARCHAR(20) NOT NULL DEFAULT 'L',
    Price DECIMAL(18,2) NOT NULL,
    Quantity INT NOT NULL DEFAULT 1,
    SubTotal DECIMAL(18,2) NOT NULL,
    CONSTRAINT FK_OrderDetails_Orders FOREIGN KEY (OrderId) REFERENCES Orders(Id) ON DELETE CASCADE,
    CONSTRAINT FK_OrderDetails_Products FOREIGN KEY (ProductId) REFERENCES Products(Id)
);
GO

-- 8. TẠO BẢNG 6: ADMINS (QUẢN TRỊ VIÊN)
CREATE TABLE Admins (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Username NVARCHAR(50) NOT NULL UNIQUE,
    Password NVARCHAR(100) NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Role NVARCHAR(50) NOT NULL DEFAULT 'Admin',
    CreatedAt DATETIME DEFAULT GETDATE()
);
GO

-- ==========================================================
-- CHÈN DỮ LIỆU MẪU (SEED DATA)
-- ==========================================================

-- Thêm Danh mục
SET IDENTITY_INSERT Categories ON;
INSERT INTO Categories (Id, Name, Description) VALUES
(1, N'Áo Thun & Sơ Mi Hè', N'Các mẫu áo phông, áo sơ mi thoáng mát phong cách trẻ trung'),
(2, N'Bộ Sưu Tập Nổi Bật', N'Các sản phẩm hot bán chạy nhất mùa hè'),
(3, N'Hàng Mới Về 2026', N'Mẫu thiết kế thời trang hiện đại mới cập bến'),
(4, N'Thời Trang Nữ & Phụ Kiện', N'Trang phục và phụ kiện cao cấp thanh lịch');
SET IDENTITY_INSERT Categories OFF;
GO

-- Thêm Sản phẩm (tương ứng với f1 -> f8, n1 -> n8 có sẵn ảnh)
SET IDENTITY_INSERT Products ON;
INSERT INTO Products (Id, CategoryId, Name, Brand, Price, OldPrice, ImageUrl, Description, Stock, IsFeatured, IsNewArrival) VALUES
-- Sản phẩm nổi bật (Featured Products - f1 đến f8)
(1, 1, N'Cartoon Astronaut T-Shirts V1', N'Adidas', 100.00, 140.00, 'img/products/f1.jpg', N'Áo thun cotton cao cấp in hình phi hành gia cartoon sắc nét, chất liệu thoáng khí, co giãn 4 chiều.', 50, 1, 0),
(2, 1, N'Cartoon Astronaut T-Shirts V2', N'Adidas', 110.00, 150.00, 'img/products/f2.jpg', N'Áo thun họa tiết cây hoa nhiệt đới mùa hè rực rỡ, mềm mại thoải mái vận động.', 45, 1, 0),
(3, 1, N'Cartoon Astronaut T-Shirts V3', N'Adidas', 105.00, 135.00, 'img/products/f3.jpg', N'Thiết kế áo phong cách vintage retro, chất vải cotton 100% thấm hút mồ hôi tối đa.', 60, 1, 0),
(4, 1, N'Cartoon Astronaut T-Shirts V4', N'Adidas', 95.00, 120.00, 'img/products/f4.jpg', N'Áo phông họa tiết phong cách đường phố cá tính, form rộng streetwear.', 30, 1, 0),
(5, 2, N'Cartoon Astronaut T-Shirts V5', N'Adidas', 125.00, 160.00, 'img/products/f5.jpg', N'Sơ mi hoa lá mùa hè hawaii nổi bật, nhẹ nhàng bay bổng.', 40, 1, 0),
(6, 2, N'Cartoon Astronaut T-Shirts V6', N'Adidas', 130.00, 170.00, 'img/products/f6.jpg', N'Áo khoác sơ mi dài tay phối hai màu cá tính, đường may chắc chắn.', 25, 1, 0),
(7, 2, N'Cartoon Astronaut T-Shirts V7', N'Adidas', 85.00, 115.00, 'img/products/f7.jpg', N'Quần short ống rộng thoải mái đi biển, túi khóa tiện lợi.', 70, 1, 0),
(8, 2, N'Cartoon Astronaut T-Shirts V8', N'Adidas', 90.00, 120.00, 'img/products/f8.jpg', N'Áo blouse nữ họa tiết thời trang thanh lịch, nhẹ nhàng.', 35, 1, 0),

-- Sản phẩm mới về (New Arrivals - n1 đến n8)
(9, 3, N'Cartoon Astronaut Shirt N1', N'Zara', 115.00, 150.00, 'img/products/n1.jpg', N'Sơ mi công sở vải Oxford cao cấp chống nhăn, tôn dáng thanh lịch.', 40, 0, 1),
(10, 3, N'Cartoon Astronaut Shirt N2', N'Zara', 120.00, 160.00, 'img/products/n2.jpg', N'Sơ mi caro xám trẻ trung, dễ phối đồ với quần jeans hoặc kaki.', 55, 0, 1),
(11, 3, N'Cartoon Astronaut Shirt N3', N'Zara', 99.00, 130.00, 'img/products/n3.jpg', N'Sơ mi trắng basic tối giản tinh tế, cổ áo đứng form chuẩn.', 80, 0, 1),
(12, 3, N'Cartoon Astronaut Shirt N4', N'Nike', 108.00, 145.00, 'img/products/n4.jpg', N'Áo polo thể thao chất vải cá sấu gai mịn, thấm hút mồ hôi nhanh.', 65, 0, 1),
(13, 4, N'Cartoon Astronaut Shirt N5', N'Nike', 112.00, 140.00, 'img/products/n5.jpg', N'Sơ mi denim xanh cá tính bền bỉ, phối wash nhẹ hiện đại.', 35, 0, 1),
(14, 4, N'Cartoon Astronaut Shirt N6', N'Gucci', 75.00, 95.00, 'img/products/n6.jpg', N'Quần soóc kaki co giãn nhẹ, phối túi tiện lợi phong cách năng động.', 50, 0, 1),
(15, 4, N'Cartoon Astronaut Shirt N7', N'Gucci', 129.00, 175.00, 'img/products/n7.jpg', N'Sơ mi vàng cát họa tiết bohemieng phá cách độc đáo.', 20, 0, 1),
(16, 4, N'Cartoon Astronaut Shirt N8', N'Adidas', 118.00, 155.00, 'img/products/n8.jpg', N'Áo sơ mi tay ngắn tone đen sang trọng, vải lụa tơ tằm siêu mềm.', 45, 0, 1);
SET IDENTITY_INSERT Products OFF;
GO

-- Thêm Khách hàng mẫu
SET IDENTITY_INSERT Customers ON;
INSERT INTO Customers (Id, FullName, Email, PhoneNumber, Address) VALUES
(1, N'Nguyễn Minh Đức', 'minhduc@gmail.com', '0912345678', N'Số 123 Đường Cầu Giấy, Hà Nội'),
(2, N'Trần Thị Mai', 'maitran@gmail.com', '0987654321', N'Số 45 Lê Lợi, Quận 1, TP. Hồ Chí Minh'),
(3, N'Lê Hoàng Long', 'longle@gmail.com', '0905123987', N'Số 88 Nguyễn Văn Linh, Đà Nẵng');
SET IDENTITY_INSERT Customers OFF;
GO

-- Thêm Hóa đơn mẫu
SET IDENTITY_INSERT Orders ON;
INSERT INTO Orders (Id, OrderCode, CustomerId, CustomerName, CustomerPhone, CustomerEmail, ShippingAddress, OrderNotes, PaymentMethod, TotalAmount, Status, OrderDate) VALUES
(1, 'MD-20261020-001', 1, N'Nguyễn Minh Đức', '0912345678', 'minhduc@gmail.com', N'Số 123 Đường Cầu Giấy, Hà Nội', N'Giao giờ hành chính', N'COD (Thanh toán khi nhận hàng)', 210.00, N'Đã hoàn thành', DATEADD(DAY, -2, GETDATE())),
(2, 'MD-20261020-002', 2, N'Trần Thị Mai', '0987654321', 'maitran@gmail.com', N'Số 45 Lê Lợi, Quận 1, TP. Hồ Chí Minh', N'Gọi trước khi giao', N'Chuyển khoản ngân hàng', 194.00, N'Đang giao', DATEADD(DAY, -1, GETDATE())),
(3, 'MD-20261020-003', 3, N'Lê Hoàng Long', '0905123987', 'longle@gmail.com', N'Số 88 Nguyễn Văn Linh, Đà Nẵng', N'Đóng gói cẩn thận', N'COD (Thanh toán khi nhận hàng)', 100.00, N'Chờ xử lý', GETDATE());
SET IDENTITY_INSERT Orders OFF;
GO

-- Thêm Chi tiết hóa đơn mẫu
SET IDENTITY_INSERT OrderDetails ON;
INSERT INTO OrderDetails (Id, OrderId, ProductId, ProductName, ProductImage, Size, Price, Quantity, SubTotal) VALUES
(1, 1, 1, N'Cartoon Astronaut T-Shirts V1', 'img/products/f1.jpg', 'L', 100.00, 1, 100.00),
(2, 1, 2, N'Cartoon Astronaut T-Shirts V2', 'img/products/f2.jpg', 'XL', 110.00, 1, 110.00),
(3, 2, 4, N'Cartoon Astronaut T-Shirts V4', 'img/products/f4.jpg', 'M', 95.00, 1, 95.00),
(4, 2, 11, N'Cartoon Astronaut Shirt N3', 'img/products/n3.jpg', 'L', 99.00, 1, 99.00),
(5, 3, 1, N'Cartoon Astronaut T-Shirts V1', 'img/products/f1.jpg', 'M', 100.00, 1, 100.00);
SET IDENTITY_INSERT OrderDetails OFF;
GO

-- Thêm Quản trị viên mẫu (Tài khoản: admin / Mật khẩu: admin123)
SET IDENTITY_INSERT Admins ON;
INSERT INTO Admins (Id, Username, Password, FullName, Role) VALUES
(1, 'admin', 'admin123', N'Quản Trị Viên Minh Đức', 'Admin');
SET IDENTITY_INSERT Admins OFF;
GO

-- ==========================================================
-- CÁC TRUY VẤN MẪU HỮU ÍCH CHO BÁO CÁO ĐỒ ÁN
-- ==========================================================

-- 1. Xem danh sách sản phẩm kèm tên danh mục
SELECT p.Id, p.Name, c.Name AS CategoryName, p.Brand, p.Price, p.OldPrice, p.Stock
FROM Products p
INNER JOIN Categories c ON p.CategoryId = c.Id
ORDER BY p.Id ASC;

-- 2. Xem toàn bộ hóa đơn kèm số lượng mặt hàng và tên khách hàng
SELECT o.Id, o.OrderCode, o.CustomerName, o.CustomerPhone, o.TotalAmount, o.Status, o.OrderDate,
       COUNT(d.Id) AS TotalItems
FROM Orders o
LEFT JOIN OrderDetails d ON o.Id = d.OrderId
GROUP BY o.Id, o.OrderCode, o.CustomerName, o.CustomerPhone, o.TotalAmount, o.Status, o.OrderDate
ORDER BY o.OrderDate DESC;

-- 3. Xem chi tiết từng món của hóa đơn cụ thể (ví dụ hóa đơn 1)
SELECT d.Id, d.ProductName, d.Size, d.Price, d.Quantity, d.SubTotal
FROM OrderDetails d
WHERE d.OrderId = 1;

-- 4. Thống kê tổng doanh thu các đơn hàng đã hoàn thành
SELECT SUM(TotalAmount) AS TongDoanhThu
FROM Orders
WHERE Status = N'Đã hoàn thành';
GO
