using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.Entities;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>
    {
    }
}
//senin sistemine giren, kayıt olan ve sistemde yetki gerektiren işlemleri yapan kişilerin dijital kimliğidir.