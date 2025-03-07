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

// Thêm dịch vụ Controllers
builder.Services.AddControllers()
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
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();

// Cấu hình Middleware

// Bật Swagger UI
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection(); // Bật HTTPS
app.UseCors("AllowAll");   // Áp dụng chính sách CORS
app.UseAuthorization();

app.MapControllers();

app.Run();
