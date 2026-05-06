namespace AdvertisementApp.Dtos.ProvidedServiceDtos

{

   public class ProvidedServiceUpdateDto : IDto
    {
        public int Id { get; set; } // Güncelleme işlemi için ID gereklidir
        public string Title { get; set; }
        public string Description { get; set; }
        public string ImagePath { get; set; } // Resim dosyasının yolu veya URL'si
    
        public DateTime CreatedDate { get; set; } // Güncelleme tarihi ekleyebiliriz, ancak genellikle bu sunucu tarafında otomatik olarak ayarlanır.
    }
    
}