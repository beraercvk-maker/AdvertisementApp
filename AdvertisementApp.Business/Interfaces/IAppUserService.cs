using AdvertisementApp.Common;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.Entities;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IAppUserService : IService<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>
    {
        // İşte eksik olan satırımız (Sözleşmeye ekliyoruz):
        Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto, int roleId);
        Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto);
    }
}