using AdvertisementApp.Dtos.AdvertisementDtos;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using AdvertisementApp.Entities;
using AdvertisementApp.Common;
namespace AdvertisementApp.Business.Interfaces 
{
   public interface IAdvertisementService : IService<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>
    {
        Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync(); // Aktif olan ilanları getiren yeni bir metot ekliyoruz. Bu metot, sadece aktif ilanları döndürecek şekilde tasarlanacak.
    }
}