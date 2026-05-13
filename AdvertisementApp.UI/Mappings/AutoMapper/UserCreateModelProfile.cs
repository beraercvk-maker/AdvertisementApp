using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Models;
using AutoMapper;


namespace AdvertisementApp.UI.Mappings.AutoMapper // Kendi klasör yoluna göre düzelt
{
    // 1. AutoMapper'ın "Profile" sınıfından miras alıyoruz (Kritik nokta)
    public class UserCreateModelProfile : Profile
    {
        // 2. Kurallarımızı Constructor (yapıcı metot) içine yazıyoruz
        public UserCreateModelProfile()
        {
           CreateMap<UserCreateModel, AppUserCreateDto>().ReverseMap(); // İki yönlü eşleştirme (ReverseMap) ekleyerek, hem UserCreateModel'den AppUserCreateDto'ya hem de AppUserCreateDto'dan UserCreateModel'e dönüşümü sağlar.
        }
    }
}