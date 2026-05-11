using AdvertisementApp.Dtos.GenderDtos;
using AdvertisementApp.Entities;

namespace AdvertisementApp.Business.Interfaces
{
    
    public interface IGenderService : IService<GenderCreateDto, GenderUpdateDto, GenderListDto, Gender>
    {
    }
}