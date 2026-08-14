using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.Services;
using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension 
    {
        public static void AddDependencies(this IServiceCollection services, string connectionString)
        {
            // 1. Veritabanı bağlantımız
            services.AddDbContext<AdvertisementContext>(options =>
            {
                options.UseSqlServer(connectionString); 
            });

            // 2. Unit of Work bağlantımız
            services.AddScoped<IUow, Uow>();
            
            // 3. VALIDATÖRLER (TAM OTOMASYON)
            // Bu tek satır, Business katmanındaki tüm validator'leri (AppUserCreateDtoValidator vb.) otomatik bulur.
            // Bu yüzden o kalabalık "AddTransient" satırlarının HEPSİNİ sildik çöpe attık!
            services.AddValidatorsFromAssembly(typeof(DependencyExtension).Assembly);
            
            // 4. AUTOMAPPER (TAM OTOMASYON - Konuştuğumuz Kısım!)
            // Tek tek ProvidedServiceProfile, AdvertisementProfile yazmaya gerek yok.
            // Bu tek satır, projeye sonradan ekleyeceğin 100 tane Profile dosyasını bile otomatik bulur.
          // Otomasyon devam ediyor, sadece versiyonun istediği cfg bloğunu ekledik:
services.AddAutoMapper(cfg => { }, typeof(DependencyExtension).Assembly);
            
            // 5. Servisler (Bunları mecburen elle yazıyoruz)
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>(); 
            services.AddScoped<IAdvertisementService, AdvertisementService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IGenderService, GenderService>();
            services.AddScoped<IAdvertisementUserService, AdvertisementUserService>();    
        }    
    }
}