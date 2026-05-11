using AdvertisementApp.Dtos.Interfaces;

namespace AdvertisementApp.Dtos.GenderDtos  
{
    public class GenderUpdateDto :IUpdateDto
    {
        public int Id { get; set; }
        public string Definition { get; set; }
    }
}