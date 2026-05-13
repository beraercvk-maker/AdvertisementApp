using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Models;
using AutoMapper;

namespace AdvertisementApp.UI.Mappings.AutoMapper
{
    public class UserLoginModelProfile : Profile
    {
        public UserLoginModelProfile()
        {
            // Arayüzdeki Model'i, arka plandaki DTO'ya dönüştürür
            CreateMap<UserLoginModel, AppUserLoginDto>();
        }
    }
}