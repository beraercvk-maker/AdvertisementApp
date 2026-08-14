using AdvertisementApp.Business.DependencyResolvers.Microsoft;
using AdvertisementApp.UI.Models;
using AdvertisementApp.UI.ValidationRules;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.Cookies; // COOKIE İÇİN EKLENDİ

var builder = WebApplication.CreateBuilder(args);

// Business katmanındaki tüm ayarları (Veritabanı, Uow, Servisler, AutoMapper ve Validasyon) yüklüyoruz.
builder.Services.AddDependencies(builder.Configuration.GetConnectionString("DefaultConnection")!);

// UI KATMANINDAKİ AUTOMAPPER AYARI (Sorunun Cevabı!)
// Bu satır UI katmanındaki "UserCreateModelProfile" gibi tüm Profile dosyalarını bulup sisteme dahil eder.
builder.Services.AddAutoMapper(cfg => { }, System.Reflection.Assembly.GetExecutingAssembly());


// COOKIE (KİMLİK DOĞRULAMA) AYARLARI
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(opt =>
    {
        opt.Cookie.Name = "AdvertisementAppCookie"; 
        opt.Cookie.HttpOnly = true; 
        opt.Cookie.SameSite = Microsoft.AspNetCore.Http.SameSiteMode.Lax; 
        opt.Cookie.SecurePolicy = Microsoft.AspNetCore.Http.CookieSecurePolicy.SameAsRequest; 
        opt.ExpireTimeSpan = TimeSpan.FromDays(20); 
        opt.SlidingExpiration = true;

        opt.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Account/SignIn"); 
        opt.LogoutPath = new Microsoft.AspNetCore.Http.PathString("/Account/LogOut"); 
        opt.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Account/AccessDenied"); 
    });

builder.Services.AddAuthorization();

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();
builder.Services.AddTransient<IValidator<UserCreateModel>, UserCreateModelValidator>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts(); 
}

app.UseHttpsRedirection();
app.UseRouting();

// MİMARİ SIRALAMA ÇOK ÖNEMLİ: Önce Kimlik, Sonra Yetki!
app.UseAuthentication(); // EKLENDİ: Sistemin "Sen Kimsin?" dediği yer
app.UseAuthorization();  // Sistemin "Yetkin Var Mı?" dediği yer

app.MapStaticAssets();
app.MapDefaultControllerRoute().WithStaticAssets();

app.Run();