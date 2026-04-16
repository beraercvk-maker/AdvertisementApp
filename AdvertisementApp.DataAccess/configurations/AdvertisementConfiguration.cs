using AdvertisementApp.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace AdvertisementApp.DataAccess.Configurations
{
    public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Advertisement> builder)
        {
            builder.Property(x => x.Title).HasMaxLength(200).IsRequired();  
            builder.Property(x => x.Description).HasMaxLength(1000).IsRequired();
            builder.Property(x => x.CreatedDate).HasDefaultValueSql("GETDATE()");
            
        
            




        }
    }
}