namespace AdvertisementApp.Entities 
{

    //Şirketin açtığı iş ilanlarını temsil eder. 
    public class Advertisement : BaseEntity
    {
        public string Title { get; set; }
        public bool Status { get; set; } // İlanın aktif mi pasif mi olduğunu belirtir
        public string Description { get; set; } // İlanın detaylı açıklaması
        public DateTime CreatedDate { get; set; } // İlanın oluşturulma tarihi
        public List<AdvertisementUser> AdvertisementUsers { get; set; } // Bir ilan birden fazla kullanıcıya sahip olabilir, bu nedenle AdvertisementAppUser listesi eklenir.
       
    }
}