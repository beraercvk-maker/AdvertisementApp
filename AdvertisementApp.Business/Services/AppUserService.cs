using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;

namespace AdvertisementApp.Business.Services
{
    public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;

        // Dependency Injection ile Mapper, Validator ve Uow nesnelerini içeri alıyoruz
        // ve "base" anahtar kelimesi ile bunları miras aldığımız ana Service sınıfına yolluyoruz.
        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator, IUow uow) 
            : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
        }

    }
}