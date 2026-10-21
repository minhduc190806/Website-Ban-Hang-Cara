using Microsoft.AspNetCore.DataProtection;
using Microsoft.EntityFrameworkCore;
using MinhDucWeb.Data;

var builder = WebApplication.CreateBuilder(args);

// 1. Thêm dịch vụ MVC Controllers và Views
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

// Cấu hình DataProtection lưu khóa trong thư mục dự án
builder.Services.AddDataProtection()
    .PersistKeysToFileSystem(new DirectoryInfo(Path.Combine(builder.Environment.ContentRootPath, "Keys")));

// 2. Thêm Session để lưu trữ Giỏ hàng
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(60);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
// 3. Cấu hình DbContext kết nối Cơ Sở Dữ Liệu (ÉP DÙNG SQL SERVER 100%)
string[] possibleServers = new[]
{
    "Lenovo-LOQ",
    "localhost",
    @"Lenovo-LOQ\SQLEXPRESS",
    @".\SQLEXPRESS",
    @"(localdb)\MSSQLLocalDB"
};

string workingSqlServerConn = builder.Configuration.GetConnectionString("SqlServerConnection") 
    ?? "Server=Lenovo-LOQ;Database=ShopMinhDucDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";

bool useSqlServer = false;
string lastError = "";

foreach (var server in possibleServers)
{
    try
    {
        var testConnStr = $"Server={server};Database=ShopMinhDucDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True;Connect Timeout=3";
        using var testConn = new Microsoft.Data.SqlClient.SqlConnection(testConnStr);
        testConn.Open();
        workingSqlServerConn = $"Server={server};Database=ShopMinhDucDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=True";
        useSqlServer = true;
        Console.WriteLine($">>> [ĐÃ TÌM THẤY SQL SERVER]: Kết nối thành công vào '{server}' <<<");
        break;
    }
    catch (Exception ex)
    {
        lastError = ex.Message;
    }
}

if (!useSqlServer)
{
    Console.WriteLine($">>> [LỖI KẾT NỐI SQL SERVER]: {lastError}");
}

// Bắt buộc dùng UseSqlServer, KHÔNG dùng UseSqlite nữa
builder.Services.AddDbContext<ShopDbContext>(options =>
{
    options.UseSqlServer(workingSqlServerConn);
});

var app = builder.Build();

// 4. Khởi tạo CSDL và Seed Data tự động khi ứng dụng chạy
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var logger = services.GetRequiredService<ILogger<Program>>();
    try
    {
        var context = services.GetRequiredService<ShopDbContext>();
        DbInitializer.Initialize(context);
        if (useSqlServer)
        {
            logger.LogInformation(">>> [CSDL KẾT NỐI THÀNH CÔNG]: Đang sử dụng Microsoft SQL Server (ShopMinhDucDB) <<<");
        }
        else
        {
            logger.LogInformation(">>> [CSDL KẾT NỐI THÀNH CÔNG]: Đang sử dụng CSDL SQLite (ShopMinhDuc.db) - 100% Sẵn Sàng! <<<");
        }
    }
    catch (Exception ex)
    {
        logger.LogError($"[LỖI CSDL]: {ex.Message}");
    }
}

// 5. Cấu hình HTTP Request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

// Cho phép tải xuống các file .sql, .doc, .docx từ wwwroot
var fileTypeProvider = new Microsoft.AspNetCore.StaticFiles.FileExtensionContentTypeProvider();
fileTypeProvider.Mappings[".sql"] = "application/sql";
fileTypeProvider.Mappings[".doc"] = "application/msword";
fileTypeProvider.Mappings[".docx"] = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";

app.UseStaticFiles(new StaticFileOptions
{
    ContentTypeProvider = fileTypeProvider
});

app.UseRouting();

app.UseSession();
app.UseAuthorization();

// 6. Cấu hình Routing cho Area Admin và User
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
