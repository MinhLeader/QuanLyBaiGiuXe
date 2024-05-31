using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using QLBGX.Models;
using QLBGX.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<QuanLyBaiGiuXeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DemoConnection")));

builder.Services.AddControllersWithViews();
// ??ng ký IParkingService và ParkingService
builder.Services.AddScoped<IParkingService, ParkingService>();
// Đăng ký TaiKhoanService và IHttpContextAccessor
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<TaiKhoanService>();


// Cấu hình xác thực sử dụng cookie
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Home/Login"; // Đường dẫn đến trang đăng nhập
        //options.AccessDeniedPath = "/Account/AccessDenied"; // Đường dẫn đến trang từ chối truy cập
    });




var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");
app.UseAuthorization();

app.Run();
