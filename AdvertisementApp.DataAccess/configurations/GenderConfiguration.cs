using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class GenderConfiguration : IEntityTypeConfiguration<Gender>
    {
        public void Configure(EntityTypeBuilder<Gender> builder)
        {
            builder.Property(x => x.Definition).HasMaxLength(50).IsRequired(); // Cinsiyet tanımının (Örn: "Erkek", "Kadın", "Diğer") alanı için kısıtlamalar eklenir.
        }
    }
}