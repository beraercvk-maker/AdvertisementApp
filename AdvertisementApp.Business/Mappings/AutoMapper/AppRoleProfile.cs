using AdvertisementApp.Dtos.AppRoleDtos;
using AdvertisementApp.Entities; // AppRole entity'sinin olduğu yer
using AutoMapper;

namespace AdvertisementApp.Business.Mappings.AutoMapper
{
    public class AppRoleProfile : Profile
    {
        public AppRoleProfile()
        {
            // Veritabanından gelen AppRole, AppRoleListDto'ya dönüşebilsin (ve tam tersi)
            CreateMap<AppRole, AppRoleListDto>().ReverseMap();
            
            // Kullanıcıdan gelen CreateDto, AppRole'e dönüşebilsin (ve tam tersi)
            CreateMap<AppRole, AppRoleCreateDto>().ReverseMap();
            
            // Kullanıcıdan gelen UpdateDto, AppRole'e dönüşebilsin (ve tam tersi)
            CreateMap<AppRole, AppRoleUpdateDto>().ReverseMap();
        }
    }
}