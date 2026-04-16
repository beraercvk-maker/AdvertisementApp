using System;

namespace AdvertisementApp.Entities
{
    // Gerçek dünyada bir insan (AppUser) birden fazla iş ilanına (Advertisement) başvurabilir. 
    // Aynı şekilde bir iş ilanına da yüzlerce insan başvurabilir. Buna Çoka Çok (Many-to-Many) ilişki denir.
    public class AdvertisementUser : BaseEntity
    {
        // 1. İlan Bağlantısı
        public int AdvertisementId { get; set; }
        public Advertisement Advertisement { get; set; }

        // 2. Kullanıcı Bağlantısı (Diyagramda UserId, bizde AppUser olduğu için AppUserId yapıyoruz)
        public int AppUserId { get; set; }
        public AppUser AppUser { get; set; }

        // 3. Başvuru Durumu Bağlantısı
        public int AdvertisementUserStatusId { get; set; } 
        public AdvertisementUserStatus AdvertisementUserStatus { get; set; }

        // 4. Askerlik Durumu Bağlantısı
        public int MilitaryStatusId { get; set; }
        public MilitaryStatus MilitaryStatus { get; set; }  

        // 5. Başvuru Detayları
        public int WorkExperience { get; set; } 
        public string CvPath { get; set; } 
        public DateTime EndDate { get; set; } 
    }

    
}