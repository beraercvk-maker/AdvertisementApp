using System.Collections.Generic;
using System.Threading.Tasks;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos.AdvertisementDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using AdvertisementApp.Common.Enums;

namespace AdvertisementApp.Business.Services
{
    public class AdvertisementService : Service<AdvertisementCreateDto, AdvertisementUpdateDto, AdvertisementListDto, Advertisement>, IAdvertisementService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;

        public AdvertisementService(IMapper mapper, IValidator<AdvertisementCreateDto> createValidator, IValidator<AdvertisementUpdateDto> updateValidator, IUow uow) : base(mapper, createValidator, updateValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
        }
        public async Task<IResponse<List<AdvertisementListDto>>> GetActivesAsync()
        {
var data = await _uow.GetRepository<Advertisement>().GetAllAsync(x => x.Status == true, x => x.CreatedDate, OrderByType.DESC); // Sadece OrderByType.DESC kaldı
            
            var dto = _mapper.Map<List<AdvertisementListDto>>(data);
            
            return new Response<List<AdvertisementListDto>>(ResponseType.Success, dto);
        }

        
    }
}