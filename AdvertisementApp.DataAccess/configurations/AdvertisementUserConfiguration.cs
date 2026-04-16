using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AdvertisementUserConfiguration : IEntityTypeConfiguration<AdvertisementUser>
    {
        public void Configure(EntityTypeBuilder<AdvertisementUser> builder)
        {
            // 1. İlan Bağlantısı
            builder.HasOne(x => x.Advertisement)
                   .WithMany(x => x.AdvertisementUsers)
                   .HasForeignKey(x => x.AdvertisementId);

            // 2. Kullanıcı Bağlantısı
            builder.HasOne(x => x.AppUser)
                   .WithMany(x => x.AdvertisementUsers)
                   .HasForeignKey(x => x.AppUserId);

            // 3. Başvuru Durumu Bağlantısı (Örn: Onaylandı, Reddedildi)
            builder.HasOne(x => x.AdvertisementUserStatus)
                   .WithMany(x => x.AdvertisementUsers)
                   .HasForeignKey(x => x.AdvertisementUserStatusId);

            // 4. Askerlik Durumu Bağlantısı
            builder.HasOne(x => x.MilitaryStatus)
                   .WithMany(x => x.AdvertisementUsers)
                   .HasForeignKey(x => x.MilitaryStatusId);

            // EXTRA GÜVENLİK: Bir kullanıcı (AppUserId), aynı ilana (AdvertisementId) sadece BİR KERE başvurabilsin!
            builder.HasIndex(x => new { x.AdvertisementId, x.AppUserId }).IsUnique();


        }
    }
}