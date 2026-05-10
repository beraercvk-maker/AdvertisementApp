using AdvertisementApp.Business.DependencyResolvers.Microsoft;

var builder = WebApplication.CreateBuilder(args);

// Business katmanındaki tüm ayarları (Veritabanı, Uow, Servisler, AutoMapper ve Validasyon) tek kalemde yüklüyoruz.
builder.Services.AddDependencies(builder.Configuration.GetConnectionString("DefaultConnection")!);

// Add services to the container.
builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapDefaultControllerRoute().WithStaticAssets();

app.Run();