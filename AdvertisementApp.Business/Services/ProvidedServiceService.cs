using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;

namespace AdvertisementApp.Business.Services
{
   public class ProvidedServiceService : Service<ProvidedServiceCreateDto, ProvidedServiceUpdateDto, ProvidedServiceListDto, ProvidedService>, IProvidedServiceService
    {
        public ProvidedServiceService(IMapper mapper, IValidator<ProvidedServiceCreateDto> createValidator, IValidator<ProvidedServiceUpdateDto> updateValidator, IUow uow) : base(mapper, createValidator, updateValidator, uow)
        {
        }
    }
}