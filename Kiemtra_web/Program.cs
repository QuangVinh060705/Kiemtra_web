using Kiemtra_web.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Lấy chuỗi kết nối từ appsettings.json
var connectionString = builder.Configuration.GetConnectionString("HanghoaDBConnectionString");

if (string.IsNullOrEmpty(connectionString))
{
    // Nếu không tìm thấy cấu hình, sử dụng chuỗi kết nối mặc định (CÓ THỂ SỬA LẠI CHO HỢP LÝ)
    connectionString = "Data Source=Vinh_Nguyen;Initial Catalog=GoodDB;User ID=sa;Password=Vinh2005@;Connect Timeout=30;Encrypt=True;Trust Server Certificate=True;Application Intent=ReadWrite;Multi Subnet Failover=False";
}

// Đăng ký DbContext với SQL Server
builder.Services.AddDbContext<HanghoaContext>(options =>
    options.UseSqlServer(connectionString));

// Đăng ký HttpClient để DI container có thể cung cấp dịch vụ HttpClient
builder.Services.AddHttpClient();  // Đăng ký HttpClient

// Thêm dịch vụ MVC (cho cả Views và Controllers)
builder.Services.AddControllersWithViews()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Giữ nguyên tên thuộc tính trong JSON
    });

// Cấu hình Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Bật CORS (nếu cần)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()     // Cho phép tất cả các nguồn
                        .AllowAnyMethod()     // Cho phép tất cả các phương thức HTTP (GET, POST, PUT, DELETE...)
                        .AllowAnyHeader());   // Cho phép tất cả các headers
});

var app = builder.Build();

// Cấu hình Middleware
app.UseRouting();

// Bật Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

// Bật HTTPS
app.UseHttpsRedirection();

// Áp dụng chính sách CORS trước Authorization
app.UseCors("AllowAll");   // Áp dụng chính sách CORS

app.UseAuthorization();

// Cấu hình Route cho MVC (bao gồm cả Controller và View)
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=HangHoaMVC}/{action=Index}/{id?}");

// Nếu bạn muốn dùng các endpoint Web API, bạn có thể sử dụng:
app.MapControllers();

app.Run();
