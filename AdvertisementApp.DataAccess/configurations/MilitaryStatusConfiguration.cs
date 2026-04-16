using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class MilitaryStatusConfiguration : IEntityTypeConfiguration<MilitaryStatus>
    {
        public void Configure(EntityTypeBuilder<MilitaryStatus> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(100).IsRequired(); // Askerlik durumunun tanımının (Örn: "Yapıldı", "Yapılmadı", "Muaf") alanı için kısıtlamalar eklenir.
        }
    }
}