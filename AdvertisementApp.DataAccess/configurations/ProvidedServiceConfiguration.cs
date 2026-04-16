using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class ProvidedServiceConfiguration : IEntityTypeConfiguration<ProvidedService>
    {
        public void Configure(EntityTypeBuilder<ProvidedService> builder)
        {
            builder.Property(x => x.Description).HasMaxLength(100).IsRequired(); // Sağlanan hizmetin tanımının (Örn: "Yemek", "Ulaşım", "Konaklama") alanı için kısıtlamalar eklenir.

            builder.Property(x=>x.Title).HasMaxLength(100).IsRequired(); // Sağlanan hizmetin başlığının (Örn: "Yemek Hizmeti", "Ulaşım Hizmeti") alanı için kısıtlamalar eklenir.
            builder.Property(x=>x.ImagePath).HasMaxLength(300).IsRequired(); // Sağlanan hizmetin görselinin dosya yolunu tutan ImagePath alanı için kısıtlamalar eklenir.
        }
    }
}