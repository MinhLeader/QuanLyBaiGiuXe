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
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
