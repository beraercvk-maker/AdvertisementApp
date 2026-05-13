using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AppRoleConfiguration : IEntityTypeConfiguration<AppRole>
    {
        public void Configure(EntityTypeBuilder<AppRole> builder)
        {
           builder.Property(x => x.Definition).HasMaxLength(300).IsRequired(); // Rolün adı (Örn: "Admin", "User") alanı için kısıtlamalar eklenir.
           builder.HasData(
                new AppRole { Id = 1, Definition = "Admin" }, // Veritabanına başlangıçta eklenmesi gereken roller tanımlanır.
                new AppRole { Id = 2, Definition = "User" }
            );
        }
    }
}