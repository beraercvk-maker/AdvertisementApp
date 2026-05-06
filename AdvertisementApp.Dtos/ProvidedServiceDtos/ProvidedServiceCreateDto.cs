namespace AdvertisementApp.Dtos
{
    //Tüm DTO'ların ortak bir arayüzü olabilir, bu da onları tanımlarken ve yönetirken tutarlılık sağlar.
    public class ProvidedServiceCreateDto : IDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } // Resim dosyasının yolu veya URL'si
    }
}