using FCVT.DAL;
using FCVT.Interfaces;
using FCVT.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
System.Net.ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login";
        options.AccessDeniedPath = "/Account/AccessDenied";
    });
// DI
builder.Services.AddScoped<DBHelper>();
builder.Services.AddScoped<IAuthen, Authen>();
builder.Services.AddScoped<IMenuService, MenuService>();
builder.Services.AddScoped<IPermissionsManagement, PermissionsManagement>();
builder.Services.AddScoped<IVehicleTracking, VehicleTracking>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");


app.Run();
