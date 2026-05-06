using AdvertisementApp.Business.DependencyResolvers.Microsoft;


var builder = WebApplication.CreateBuilder(args);

// 1. DÜZELTME: Sonuna eklediğimiz "!" işareti boş gelmeyeceğini garanti eder (Uyarıyı çözer)
builder.Services.AddDependencies(builder.Configuration.GetConnectionString("DefaultConnection")!);

// 2. DÜZELTME: AutoMapper'a Business katmanını taraması için o katmandan bir referans veriyoruz (Hatayı çözer)
builder.Services.AddAutoMapper(opt =>
{
    // Business katmanındaki (DependencyExtension'ın bulunduğu yer) tüm kural dosyalarını otomatik bulur
    opt.AddMaps(typeof(DependencyExtension).Assembly);
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
