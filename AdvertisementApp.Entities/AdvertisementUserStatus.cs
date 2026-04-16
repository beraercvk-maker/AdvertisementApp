namespace AdvertisementApp.Entities
{
    //Kullanıcıların ilanlara başvuru durumlarını temsil eder. Örneğin, bir kullanıcı bir ilana başvurduğunda, bu sınıf aracılığıyla başvuru durumunu (örneğin, "Başvuruldu", "Değerlendiriliyor", "Kabul Edildi", "Reddedildi") takip edebilirsin.
    public class AdvertisementUserStatus : BaseEntity
    {
        public string Definition { get; set; } // Başvuru durumunun adı (Örn: "Başvuruldu", "Değerlendiriliyor", "Kabul Edildi", "Reddedildi")

        public List<AdvertisementUser> AdvertisementUsers { get; set; } // Bir başvuru durumunun birden fazla kullanıcıya sahip olabileceği için AdvertisementUser listesi eklenir.
    }
}