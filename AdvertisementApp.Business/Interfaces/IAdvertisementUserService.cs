using AdvertisementApp.Common;
using AdvertisementApp.Dtos.AdvertisementUserDtos;
using AdvertisementApp.Entities;

namespace AdvertisementApp.Business.Interfaces
{
    public interface IAdvertisementUserService
    {
        Task<IResponse<AdvertisementUserCreateDto>> CreateAsync(AdvertisementUserCreateDto dto);
    }
}
