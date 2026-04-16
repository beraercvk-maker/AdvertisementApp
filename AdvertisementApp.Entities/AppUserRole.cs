namespace AdvertisementApp.Entities
{



    //"Hangi kullanıcı hangi role sahip?" 
    // AppUserRole tablosu, AppUser ve AppRole tabloları arasında bir ilişki tablosudur.
    public class AppUserRole : BaseEntity
    {
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        public int AppRoleId { get; set; }
        public AppRole AppRole { get; set; }
 
        public List<AppUserRole> UserRoles { get; set; }
    }
}