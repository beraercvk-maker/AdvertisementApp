using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AppUserRoleConfiguration : IEntityTypeConfiguration<AppUserRole>
    {
        public void Configure(EntityTypeBuilder<AppUserRole> builder)
        {
            // "Bu tablonun 1 tane AppUser'ı vardır (HasOne), 
            //  o AppUser'ın ise birden çok AppUserRole'ü olabilir (WithMany)"
            builder.HasOne(x => x.AppUser)
                   .WithMany(x => x.AppUserRoles)
                   .HasForeignKey(x => x.AppUserId);

            // Aynı mantık Rol için de geçerli
            builder.HasOne(x => x.AppRole)
                   .WithMany(x => x.UserRole)
                   .HasForeignKey(x => x.AppRoleId);

            // EXTRA GÜVENLİK: Bir kullanıcıya aynı rol (Örn: Admin) iki kere verilemesin!
            // Veritabanında AppUserId ve AppRoleId ikilisi benzersiz (Unique) olsun.
            builder.HasIndex(x => new { x.AppUserId, x.AppRoleId }).IsUnique();
        }
    }
}