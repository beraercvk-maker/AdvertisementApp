using AdvertisementApp.Business.ValidationRules;
using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace AdvertisementApp.Business.DependencyResolvers.Microsoft
{
    public static class DependencyExtension 
    {
        public static void AddDependencies(this IServiceCollection services, string connectionString)
        {
            // Veritabanı bağlantımız
            services.AddDbContext<AdvertisementContext>(options =>
            {
                options.UseSqlServer(connectionString); 
            });

            // Unit of Work bağlantımız
            services.AddScoped<IUow, Uow>();
            
            // Not: AutoMapper ayarını buradan sildik çünkü onu UI (Program.cs) katmanına taşıdık!
            services.AddTransient<IValidator<ProvidedServiceCreateDto>, ProvidedServiceCreateDtoValidator>(); // Validatörlerimizi ekliyoruz
            services.AddTransient<IValidator<ProvidedServiceUpdateDto>, ProvidedServiceUpdateDtoValidator>(); // Diğer validatörler de benzer şekilde eklenebilir
            services.AddValidatorsFromAssembly(typeof(DependencyExtension).Assembly);
        }    
    }

   
}