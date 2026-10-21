# HƯỚNG DẪN KẾT NỐI VÀ VẬN HÀNH CƠ SỞ DỮ LIỆU (CSDL)
## Dự Án Website Bán Hàng Thời Trang Cara - Minh Đức TH29.25

---

### 1. THÔNG TIN THIẾT KẾ CƠ SỞ DỮ LIỆU
File kịch bản SQL hoàn chỉnh: **`ShopMinhDucDB.sql`** (Nằm tại thư mục gốc của dự án).
Hệ thống CSDL gồm **06 bảng quan hệ chi tiết** (vượt yêu cầu tối thiểu $\ge 3$ bảng của đồ án):

1. **`Categories` (Danh mục sản phẩm):**
   - `Id` (Khóa chính), `Name` (Tên danh mục), `Description`, `CreatedAt`.
2. **`Products` (Sản phẩm bán hàng):**
   - `Id` (Khóa chính), `CategoryId` (Khóa ngoại liên kết `Categories`), `Name`, `Brand`, `Price`, `OldPrice`, `ImageUrl`, `Stock`, `IsFeatured`, `IsNewArrival`.
3. **`Customers` (Khách hàng):**
   - `Id` (Khóa chính), `FullName`, `Email`, `PhoneNumber`, `Address`, `CreatedAt`.
4. **`Orders` (Hóa đơn / Đơn hàng):**
   - `Id` (Khóa chính), `OrderCode` (Mã đơn hàng duy nhất), `CustomerId` (Khóa ngoại liên kết `Customers`), `CustomerName`, `CustomerPhone`, `CustomerAddress`, `TotalAmount`, `PaymentMethod`, `OrderStatus`, `OrderDate`.
5. **`OrderDetails` (Chi tiết đơn hàng):**
   - `Id` (Khóa chính), `OrderId` (Khóa ngoại liên kết `Orders`), `ProductId` (Khóa ngoại liên kết `Products`), `Size`, `Quantity`, `UnitPrice`, `TotalPrice`.
6. **`Admins` (Tài khoản Quản trị viên):**
   - `Id` (Khóa chính), `Username`, `PasswordHash`, `FullName`, `Role`, `CreatedAt`.

---

### 2. CÁCH KẾT NỐI VÀ CHẠY DỰ ÁN

#### CÁCH 1: Chạy với Microsoft SQL Server (Khuyên dùng khi báo cáo đồ án)
1. Mở phần mềm **SQL Server Management Studio (SSMS)**.
2. Đăng nhập vào SQL Server của máy tính bạn (Server Name thường là `.` hoặc `localhost` hoặc `.\SQLEXPRESS`).
3. Mở file **`ShopMinhDucDB.sql`** trong SSMS và bấm nút **Execute (F5)** để tự động tạo Database `ShopMinhDucDB`, các bảng và dữ liệu mẫu.
4. Mở file **`MinhDucWeb/appsettings.json`** kiểm tra chuỗi kết nối:
   ```json
   "DatabaseProvider": "SqlServer",
   "ConnectionStrings": {
     "SqlServerConnection": "Server=localhost;Database=ShopMinhDucDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True"
   }
   ```
   *(Lưu ý: Nếu SQL Server máy bạn có tên instance, ví dụ `DESKTOP-ABC\SQLEXPRESS`, hãy đổi `Server=localhost` thành `Server=.\\SQLEXPRESS`)*.
5. Nhấp đúp vào file **`Chay-Website-MinhDuc.bat`** (hoặc mở terminal gõ `cd MinhDucWeb && dotnet run`).
6. Trình duyệt mở `https://localhost:5001` để xem website kết nối CSDL trực tiếp.

---

#### CÁCH 2: Chạy Tự Động với SQLite (Không cần cài SQL Server)
Phù hợp khi đem máy đi chấm bài hoặc máy giáo viên chưa cài sẵn SQL Server:
1. Mở file **`MinhDucWeb/appsettings.json`**, đổi:
   ```json
   "DatabaseProvider": "Sqlite"
   ```
2. Nhấp đúp vào file **`Chay-Website-MinhDuc.bat`**.
3. Dự án sẽ tự động sinh file CSDL `ShopMinhDuc.db` với đầy đủ 6 bảng và dữ liệu sẵn sàng!

---

#### CÁCH 3: Trải nghiệm Nhanh Giao Diện Tĩnh (HTML/JS)
1. Nhấp đúp mở trực tiếp file **`index.html`** hoặc **`shop.html`** trên trình duyệt (Chrome, Edge, Cốc Cốc).
2. Dữ liệu Thêm / Sửa / Xóa (CRUD) được lưu trữ qua `localStorage` của trình duyệt.
3. Trong trang **`admin.html`**, có nút **"Tải SQL Script"** màu xanh để tải hoặc xem trực tiếp mã nguồn SQL CSDL 6 bảng.
