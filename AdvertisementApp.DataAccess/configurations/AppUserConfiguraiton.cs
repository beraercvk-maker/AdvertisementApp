using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AppUserConfiguration : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            // Kullanıcı Adı ayarları
            builder.Property(x => x.UserName)
                   .HasMaxLength(300) 
                   .IsRequired();     // Boş geçilemez (NOT NULL)

            // Şifre ayarları
            builder.Property(x => x.Password)
                   .HasMaxLength(64)
                   .IsRequired();

            // Telefon numarası ayarları
            builder.Property(x => x.PhoneNumber)
                   .HasMaxLength(20)
                   .IsRequired();

            // Email ayarları
            builder.Property(x => x.Email)
                   .HasMaxLength(200)
                   .IsRequired();

            // Cinsiyet ilişkisi (GenderId) zorunlu olsun
            builder.Property(x => x.GenderId).IsRequired();

            // EXTRA GÜVENLİK: Aynı mail adresiyle iki farklı kişi kayıt olamasın!
            // Veritabanında Email kolonu benzersiz (Unique) olsun.
            builder.HasIndex(x => x.Email).IsUnique(); 
        }
    }
}