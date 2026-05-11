using AdvertisementApp.Dtos.Interfaces; // Varsa interface'i ekliyoruz

namespace AdvertisementApp.Dtos.AdvertisementDtos
{
    public class AdvertisementCreateDto : IDto
    {
        // Kullanıcının formda dolduracağı başlık alanı
        public string Title { get; set; }
        
        // Kullanıcının formda dolduracağı açıklama alanı
        public string Description { get; set; }

        public bool Status { get; set; } 

        // Eğer resim ekleme vb. özellikler varsa buraya eklenebilir:
        // public string ImagePath { get; set; }
    }
}