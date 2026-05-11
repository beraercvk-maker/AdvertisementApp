using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Business.Mappings.AutoMapper;
using AdvertisementApp.Business.Services;
using AdvertisementApp.Business.ValidationRules;
using AdvertisementApp.Business.ValidationRules.FluentValidation;
using AdvertisementApp.DataAccess.Context;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.DataAccess.UnitOfWork;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.AdvertisementDtos;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.Dtos.GenderDtos;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using AdvertisementApp.Entities;
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
            services.AddAutoMapper(cfg => { }, typeof(AdvertisementProfile).Assembly );
            services.AddAutoMapper(cfg => { }, typeof(AppUserProfile).Assembly);

            // Servislerimizi ekliyoruz
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>(); 

            services.AddTransient<IValidator<AdvertisementCreateDto>, AdvertisementCreateDtoValidator>();

            services.AddTransient<IValidator<AdvertisementUpdateDto>, AdvertisementUpdateDtoValidator>();

            services.AddScoped<IAdvertisementService, AdvertisementService>();

            services.AddTransient<IValidator<AppUserCreateDto>, AppUserCreateDtoValidator>();

            services.AddTransient<IValidator<AppUserUpdateDto>, AppUserUpdateDtoValidator>();

            services.AddScoped<IAppUserService, AppUserService>();

            services.AddTransient<IValidator<GenderCreateDto>, GenderCreateDtoValidator>();

            services.AddTransient<IValidator<GenderUpdateDto>, GenderUpdateDtoValidator>();

           services.AddScoped<IGenderService, GenderService>();    


        }    
    }
}