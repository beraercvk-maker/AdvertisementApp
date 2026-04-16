using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AdvertisementUserStatusConfiguration : IEntityTypeConfiguration<AdvertisementUserStatus>
    {
        public void Configure(EntityTypeBuilder<AdvertisementUserStatus> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(100).IsRequired(); // Başvuru durumunun adı (Örn: "Başvuruldu", "Değerlendiriliyor", "Kabul Edildi", "Reddedildi") alanı için kısıtlamalar eklenir.
        }
    }
}