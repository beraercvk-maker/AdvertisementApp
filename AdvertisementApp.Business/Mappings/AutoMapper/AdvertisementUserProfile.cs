using AdvertisementApp.Dtos.AdvertisementUserDtos;
using AdvertisementApp.Entities;
using AutoMapper;

namespace AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AdvertisementUserProfile : Profile
    {
        public AdvertisementUserProfile()
        {
            CreateMap<AdvertisementUser, AdvertisementUserCreateDto>().ReverseMap();
        }
    }
}
