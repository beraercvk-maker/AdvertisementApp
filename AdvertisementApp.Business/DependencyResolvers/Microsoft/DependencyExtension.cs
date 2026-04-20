using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementApp.DataAccess.Extensions
{
    // 1. DÜZELTME: Sınıf artık "static"
    public static class DependencyExtension 
    {
        // 2. DÜZELTME: "this" kelimesi ve "connectionString" eklendi
        public static void AddDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AdvertisementContext>(options =>
            {
                // 3. DÜZELTME: Boş tırnaklar yerine parametreyi kullanıyoruz
                options.UseSqlServer(connectionString); 
            });

            services.AddScoped<IUow, Uow>();
        }    
    }    
}