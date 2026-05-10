using AdvertisementApp.Common;

using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementApp.Entities;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.Interfaces; // BaseEntity için

namespace AdvertisementApp.Business.Interfaces
{
    // T tipini de ekledik
    public interface IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity, new() // BaseEntity ve new() kısıtlamasını ekledik
    {
        Task<IResponse<CreateDto>> CreateAsync(CreateDto dto); 
        Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto); 
        Task<IResponse<ListDto>> GetByIdAsync(int id); 
        Task<IResponse> RemoveAsync(int id); 
        Task<IResponse<List<ListDto>>> GetAllAsync(); 
    }
}