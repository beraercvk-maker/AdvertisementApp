namespace AdvertisementApp.Dtos.AdvertisementDtos
{
    public class AdvertisementListDto : IDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool Status { get; set; } // Aktif/Pasif durumu
    } 
}