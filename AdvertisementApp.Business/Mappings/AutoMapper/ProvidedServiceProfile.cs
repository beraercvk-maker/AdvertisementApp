using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using AdvertisementApp.Entities;
using AutoMapper;

namespace AdvertisementApp.Business.Mappings.AutoMapper
{
    public class ProvidedServiceProfile : Profile
    {
        public ProvidedServiceProfile()
        {
            CreateMap<ProvidedServiceCreateDto, ProvidedService>().ReverseMap(); // ReverseMap() ile iki yönlü dönüşümü sağlıyoruz, yani hem CreateDto'dan Entity'ye hem de Entity'den CreateDto'ya dönüşüm mümkün olacak
            CreateMap<ProvidedServiceUpdateDto, ProvidedService>().ReverseMap();
            CreateMap<ProvidedServiceListDto, ProvidedService>().ReverseMap();
        }
    }
}