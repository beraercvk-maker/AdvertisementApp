using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos.GenderDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;

namespace AdvertisementApp.Business.Services
{
    public class GenderService : Service<GenderCreateDto, GenderUpdateDto, GenderListDto, Gender>, IGenderService
    {
        // Parametre sırasını miras alınan (base) Service sınıfının beklediği standart sıraya çektik
        public GenderService(IMapper mapper, IValidator<GenderCreateDto> createValidator, IValidator<GenderUpdateDto> updateValidator, IUow uow) 
            : base(mapper, createValidator, updateValidator, uow)
        {
           
        }
    }
}