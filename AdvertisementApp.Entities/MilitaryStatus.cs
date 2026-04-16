namespace AdvertisementApp.Entities
{
    //Kullanıcıların ilanlara başvuru durumlarını temsil eder. Örneğin, bir kullanıcı bir ilana başvurduğunda, bu sınıf aracılığıyla başvuru durumunu (örneğin, "Başvuruldu", "Değerlendiriliyor", "Kabul Edildi", "Reddedildi") takip edebilirsin.
    public class MilitaryStatus : BaseEntity
    {
        public string Definition { get; set; } // Askerlik durumunun adı (Örn: "Yapıldı", "Yapılmadı", "Muaf")

       public List<AdvertisementUser> AdvertisementUsers { get; set; } // Bir askerlik durumunun birden fazla kullanıcıya sahip olabileceği için AdvertisementUser listesi eklenir.

    }
}