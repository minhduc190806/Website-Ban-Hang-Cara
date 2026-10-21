# HƯỚNG DẪN SỬ DỤNG VÀ BÁO CÁO ĐỒ ÁN WEBSITE THƯƠNG MẠI ĐIỆN TỬ
**Đồ án:** Website Bán Hàng Thời Trang MinhDuc (MinhDucTH29.25)  
**Công nghệ:** HTML5, CSS3, JavaScript | ASP.NET Core MVC (C#) | Microsoft SQL Server (EF Core)

---

## 1. TỔNG QUAN KIẾN TRÚC DỰ ÁN

Dự án gồm đầy đủ các trang và chức năng phục vụ đồ án môn học Web:
1. **Trang Chủ (`index.html`)**: Hero banner, tính năng nổi bật, sản phẩm nổi bật, sản phẩm mới về, pop-up ưu đãi.
2. **Cửa Hàng (`shop.html`)**: Danh sách 16 sản phẩm, tìm kiếm từ khóa, lọc theo danh mục, phân trang.
3. **Chi Tiết Sản Phẩm (`sproduct.html`)**: Hình ảnh lớn, chọn ảnh thu nhỏ, chọn Size (S/M/L/XL/XXL), số lượng, thêm giỏ hàng, **sản phẩm tương tự dàn hàng ngang 4 sản phẩm/hàng**.
4. **Giỏ Hàng (`cart.html`)**: Bảng giỏ hàng, cập nhật số lượng, xóa từng món, xóa tất cả, mã giảm giá `MINHDUC50`, tổng thanh toán, nút đặt hàng.
5. **Thanh Toán / Đặt Hàng (`checkout.html`)**: Form thông tin người nhận, chọn phương thức COD / chuyển khoản, lưu vào bảng Khách hàng (`Customers`) và Hóa đơn (`Orders`, `OrderDetails`).
6. **Đặt Hàng Thành Công (`order-success.html`)**: Mã đơn hàng sinh tự động, thông tin đơn.
7. **Đăng Nhập / Đăng Ký (`login.html`)**: Form đăng nhập khách hàng và tài khoản Admin, tự động nhận diện quyền và lưu trạng thái đăng nhập.
8. **Blog Thời Trang (`blog.html`)**: 6 bài viết thời trang thực tế với hình ảnh, mẹo phối đồ hè, hướng dẫn chọn size.
9. **Giới Thiệu (`about.html`)**: Câu chuyện thương hiệu, video thực tế cửa hàng (`img/about/1.mp4`), các cam kết chất lượng.
10. **Liên Hệ (`contact.html`)**: Bản đồ, thông tin liên hệ, form gửi thắc mắc trực tuyến.
11. **Quản Trị Admin (`admin.html` hoặc `/Admin/Products`)**: Dashboard thống kê doanh thu; **Quản lý Sản phẩm (CRUD Thêm - Sửa - Xóa)**; Quản lý Hóa đơn; Quản lý Danh mục.

---

## 2. TÀI KHOẢN ĐĂNG NHẬP KIỂM THỬ

Tại trang [`login.html`](login.html), bạn có thể sử dụng các tài khoản mẫu sau:

- **Tài khoản Quản trị viên (Admin):**
  - **Tên đăng nhập:** `admin`
  - **Mật khẩu:** `admin123`
  - *(Sau khi đăng nhập thành công, hệ thống tự động chuyển thẳng vào trang Quản trị CRUD `admin.html`)*

- **Tài khoản Khách hàng:**
  - **Email:** `minhduc@gmail.com`
  - **Mật khẩu:** `123456`
  - *(Sau khi đăng nhập thành công, Navbar sẽ hiển thị tên khách hàng và nút [Thoát])*

---

## 3. THIẾT KẾ CƠ SỞ DỮ LIỆU CHI TIẾT (6 BẢNG)

Mô hình dữ liệu quan hệ 3NF chuẩn mực đáp ứng yêu cầu tối thiểu từ 03 bảng trở lên:
```
[Categories] (1) --------< (N) [Products]
                                  |
                                  | (1)
                                  v (N)
[Customers]  (1) --------< (N) [Orders] (1) --------< (N) [OrderDetails]
```

1. **`Categories` (Danh mục):** `Id (PK)`, `Name`, `Description`, `CreatedAt`
2. **`Products` (Sản phẩm):** `Id (PK)`, `CategoryId (FK)`, `Name`, `Brand`, `Price`, `OldPrice`, `ImageUrl`, `Description`, `Stock`, `IsFeatured`, `IsNewArrival`, `CreatedAt`
3. **`Customers` (Khách hàng):** `Id (PK)`, `FullName`, `Email`, `PhoneNumber`, `Address`, `CreatedAt`
4. **`Orders` (Hóa đơn / Đơn hàng):** `Id (PK)`, `OrderCode (Unique)`, `CustomerId (FK)`, `CustomerName`, `CustomerPhone`, `CustomerEmail`, `ShippingAddress`, `OrderNotes`, `PaymentMethod`, `TotalAmount`, `Status`, `OrderDate`
5. **`OrderDetails` (Chi tiết hóa đơn):** `Id (PK)`, `OrderId (FK)`, `ProductId (FK)`, `ProductName`, `ProductImage`, `Size`, `Price`, `Quantity`, `SubTotal`
6. **`Admins` (Tài khoản Quản trị):** `Id (PK)`, `Username`, `Password`, `FullName`, `Role`

> File script SQL hoàn chỉnh: **[`ShopMinhDucDB.sql`](ShopMinhDucDB.sql)**.

---

## 4. HƯỚNG DẪN MỞ VÀ TRẢI NGHIỆM

### Cách 1: Mở trực tiếp bằng trình duyệt (Nhanh nhất)
1. Mở thư mục máy tính: `C:\Users\ADMIN\Downloads\minhducth29.25`.
2. Nhấp đúp chuột vào file [`index.html`](index.html) hoặc [`shop.html`](shop.html) để mở trên Chrome/Edge.
3. Tất cả các liên kết trên **Header** và **Footer** (Trang Chủ, Giới thiệu, Cửa hàng, Blog, Liên hệ, Đăng nhập, Giỏ hàng, Admin) đều chuyển trang chính xác.
4. Bấm vào sản phẩm để xem chi tiết → chọn size → thêm vào giỏ → xem giỏ hàng → thanh toán.
5. Phần **Sản phẩm tương tự** ở cuối trang chi tiết đã được dàn hàng ngang 4 sản phẩm chuẩn đẹp.

### Cách 2: Chạy phiên bản ASP.NET Core MVC & CSDL SQL Server (Khi nộp báo cáo)
1. Nạp file [`ShopMinhDucDB.sql`](ShopMinhDucDB.sql) vào SSMS (Execute F5).
2. Mở file [`MinhDucTH29.25.slnx`](MinhDucTH29.25.slnx) bằng Visual Studio 2022 và bấm **F5**.
   Hoặc chạy bằng lệnh:
   ```powershell
   cd c:\Users\ADMIN\Downloads\minhducth29.25\MinhDucWeb
   dotnet run
   ```
