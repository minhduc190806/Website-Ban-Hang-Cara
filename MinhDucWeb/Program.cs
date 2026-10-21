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

// 3. Cấu hình DbContext kết nối Cơ Sở Dữ Liệu
var provider = builder.Configuration.GetValue<string>("DatabaseProvider") ?? "SqlServer";
var sqlServerConn = builder.Configuration.GetConnectionString("SqlServerConnection");
var sqliteConn = builder.Configuration.GetConnectionString("SqliteConnection");

bool useSqlServer = false;
if (provider.Equals("SqlServer", StringComparison.OrdinalIgnoreCase) && !string.IsNullOrEmpty(sqlServerConn))
{
    try
    {
        var connBuilder = new Microsoft.Data.SqlClient.SqlConnectionStringBuilder(sqlServerConn)
        {
            ConnectTimeout = 2 // Thử kết nối nhanh trong 2 giây
        };
        using var testConn = new Microsoft.Data.SqlClient.SqlConnection(connBuilder.ConnectionString);
        testConn.Open();
        useSqlServer = true;
    }
    catch
    {
        useSqlServer = false;
    }
}

builder.Services.AddDbContext<ShopDbContext>(options =>
{
    if (useSqlServer)
    {
        options.UseSqlServer(sqlServerConn);
    }
    else
    {
        options.UseSqlite(sqliteConn);
    }
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

app.UseStaticFiles();

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
