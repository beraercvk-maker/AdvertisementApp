using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.Mappings.AutoMapper;
using AdvertisementApp.Business.Services;
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
            
            // Validatörleri otomatik tarayarak ekliyoruz (Tek tek AddTransient yazmaya gerek kalmadı!)
            services.AddValidatorsFromAssembly(typeof(DependencyExtension).Assembly);
            
            // AutoMapper kaydı (AutoMapper 16+ kurallarına uygun güncel hali)
            services.AddAutoMapper(cfg => { }, typeof(ProvidedServiceProfile).Assembly); 

            // Servislerimizi ekliyoruz
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>(); 
        }    
    }
}