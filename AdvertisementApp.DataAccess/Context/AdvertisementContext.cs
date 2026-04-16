using AdvertisementApp.DataAccess.Configurations;
using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;

namespace AdvertisementApp.DataAccess.Context
{
    
    public class AdvertisementContext : DbContext
    {
        public AdvertisementContext(DbContextOptions<AdvertisementContext> options) : base(options) // DbContext sinifinin yapici metodunu çağirarak, veritabani bağlanti seçeneklerini alir ve bu seçenekleri DbContext'e iletir.
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
            modelBuilder.ApplyConfiguration(new AppUserConfiguration());
            modelBuilder.ApplyConfiguration(new AppRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AppUserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertisementConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertisementUserConfiguration());
            modelBuilder.ApplyConfiguration(new ProvidedServiceConfiguration());
            modelBuilder.ApplyConfiguration(new GenderConfiguration());
            modelBuilder.ApplyConfiguration(new AdvertisementUserStatusConfiguration());
            modelBuilder.ApplyConfiguration(new MilitaryStatusConfiguration());
        }
            //tabloya erişim sağlamak için DbSet tanımlamaları yapılır. DbSet, Entity Framework Core'da bir tabloyu temsil eder. Her DbSet, veritabanındaki bir tabloya karşılık gelir ve bu tabloda işlem yapmamızı sağlar.
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppUserRole> UserRoles { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<AdvertisementUser> AdvertisementUsers { get; set; }
        public DbSet<ProvidedService> ProvidedServices { get; set; }
        public DbSet<Gender> Genders { get; set; }
        public DbSet<AdvertisementUserStatus> AdvertisementUserStatuses { get; set; }
        public DbSet<MilitaryStatus> MilitaryStatuses { get; set; }

        //dotnet add AdvertisementApp.DataAccess reference AdvertisementApp.Entities  kodu ile entities den referans aldık.
        //public DbSet<AppUser> Users dediğimizde, SQL'de ismi Users olan ve kolonları AppUser sınıfındaki özelliklerden oluşan bir tablo oluşacak.
    }
}