//AutoMapper kütüphanesinde eşleştirme (mapping) kurallarının yazıldığı sınıfların kalıbına Profile denir. Tıpkı bir ayar dosyası veya kimlik profili gibi.


using AdvertisementApp.Dtos.AdvertisementDtos;
using AdvertisementApp.Entities; // Kendi Entity namespace'ine göre kontrol et
using AutoMapper;

namespace AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AdvertisementProfile : Profile
    {
        public AdvertisementProfile()
        {
            // Veritabanı tablosu (Advertisement) ile List DTO'su arasında iki yönlü dönüştürme
            CreateMap<Advertisement, AdvertisementListDto>().ReverseMap();
            
            // Veritabanı tablosu ile Create DTO'su arasında iki yönlü dönüştürme
            CreateMap<Advertisement, AdvertisementCreateDto>().ReverseMap();
            
            // Veritabanı tablosu ile Update DTO'su arasında iki yönlü dönüştürme
            CreateMap<Advertisement, AdvertisementUpdateDto>().ReverseMap();
        }
    }
}