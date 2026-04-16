namespace AdvertisementApp.Entities
{
    public class AppRole : BaseEntity
    {
        
        public string Definition { get; set; }// Rolün adı (Örn: "Admin", "Member")

        public List<AppUserRole> UserRole { get; set; } // Bir rolün birden fazla kullanıcıya sahip olabileceği için AppUserRole listesi eklenir.
    }
}