namespace AdvertisementApp.Entities
{

    //Senin sistemine kayıt olan gerçek insanları (kullanıcıları) temsil eder. Adı, şifresi, maili burada tutulur.
    public class AppUser : BaseEntity
    {
        public string UserName { get; set; }
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public Gender Gender { get; set; }  // AppUser ile Gender arasında ilişki kurmak için Gender nesnesi eklenir.
        public int GenderId { get; set; } // AppUser tablosunda GenderId alanı eklenir. Bu alan, AppUser ile Gender tablosu arasında ilişki kurmak için kullanılır.
        public List<AppUserRole> AppUserRoles { get; set; } // Bir kullanıcının birden fazla role sahip olabileceği için AppUserRole listesi eklenir.
        public List<AdvertisementUser> AdvertisementUsers { get; set; } // Bir kullanıcının birden fazla ilana sahip olabileceği için AdvertisementAppUser listesi eklenir.

        
    }
}