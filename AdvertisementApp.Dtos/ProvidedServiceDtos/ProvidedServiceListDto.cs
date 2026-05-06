namespace AdvertisementApp.Dtos.ProvidedServiceDtos
{
    //Tüm DTO'ların ortak bir arayüzü olabilir, bu da onları tanımlarken ve yönetirken tutarlılık sağlar.
    public class ProvidedServiceListDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        
        public string ImagePath { get; set; }

        public string Description { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}