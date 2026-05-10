using AdvertisementApp.Business.Extensions;
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.Interfaces;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AdvertisementApp.Business.Services
{
    public class Service<CreateDto, UpdateDto, ListDto, T> : IService<CreateDto, UpdateDto, ListDto, T>
        where CreateDto : class, IDto, new()
        where UpdateDto : class, IUpdateDto, new()
        where ListDto : class, IDto, new()
        where T : BaseEntity, new()
    {
        private readonly IMapper _mapper;
        private readonly IValidator<CreateDto> _createValidator;
        private readonly IValidator<UpdateDto> _updateValidator;
        private readonly IUow _uow;

        public Service(IMapper mapper, IValidator<CreateDto> createValidator, IValidator<UpdateDto> updateValidator, IUow uow)
        {
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _uow = uow;
        }

        public async Task<IResponse<CreateDto>> CreateAsync(CreateDto dto) // async yapıya çevrildi ve bu metodun dönüş tipi Task<IResponse<CreateDto>> olarak değiştirildi
        {
            var result = _createValidator.Validate(dto);

            if (result.IsValid)
            {
                var entity = _mapper.Map<T>(dto);
                await _uow.GetRepository<T>().CreateAsync(entity);
                await _uow.SaveChangesAsync();
                var responseDto = _mapper.Map<CreateDto>(entity);

                return new Response<CreateDto>(ResponseType.Success, responseDto);
            }
            else
            {
                return new Response<CreateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
            }
        }

        // async ve await kullanılarak performanslı hale getirildi
        public async Task<IResponse<List<ListDto>>> GetAllAsync()
        {
            var data = await _uow.GetRepository<T>().GetAllAsync(); // Veritabanından tüm verileri asenkron olarak çekiyoruz
            var dto = _mapper.Map<List<ListDto>>(data); // Veritabanından gelen verileri ListDto'ya dönüştürüyoruz
            return new Response<List<ListDto>>(ResponseType.Success, dto); 
        }

        // IDto yerine IService'deki imzaya uygun olarak ListDto kullanıldı
        public async Task<IResponse<ListDto>> GetByIdAsync(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == id);
            if (data != null)
            {
                var dto = _mapper.Map<ListDto>(data);
                return new Response<ListDto>(ResponseType.Success, dto);
            }
            else
            {
                // default(IDto) yerine null dönebiliriz ya da mesaj verebiliriz
                return new Response<ListDto>(ResponseType.NotFound, $"Id'si {id} olan kayıt bulunamadı.");
            }
        }

        // Süslü parantezler ve async/await yapısı düzeltildi
        public async Task<IResponse> RemoveAsync(int id)
        {
            var data = await _uow.GetRepository<T>().GetByFilterAsync(x => x.Id == id); //
            if (data == null)
            {
                return new Response(ResponseType.NotFound, $"Id'si {id} olan veri bulunamadı.");
            }

            _uow.GetRepository<T>().Remove(data);
            await _uow.SaveChangesAsync();
            return new Response(ResponseType.Success, $"Id'si {id} olan veri başarıyla silindi.");
        }

        // async yapıya çevrildi ve Task.FromResult kaldırıldı
        public async Task<IResponse<UpdateDto>> UpdateAsync(UpdateDto dto)
        {
            var result = _updateValidator.Validate(dto);
            
            if (result.IsValid) 
            {
                var unchangedData =await _uow.GetRepository<T>().FindAsync(dto.Id);  // DTO'dan Entity'e dönüştürme
                if (unchangedData == null)
                {
                    return new Response<UpdateDto>(ResponseType.NotFound, $"Id'si {dto.Id} olan veri bulunamadı.");
                }
                _mapper.Map(dto, unchangedData); // DTO'daki verileri mevcut Entity'ye kopyala
                _uow.GetRepository<T>().Update(unchangedData); // Veritabanında güncelleme işlemi
                await _uow.SaveChangesAsync(); // SaveChangesAsync yapıldı
                var responseDto = _mapper.Map<UpdateDto>(unchangedData); 

                return new Response<UpdateDto>(ResponseType.Success, responseDto);
            }
            else
            {
                return new Response<UpdateDto>(ResponseType.ValidationError, dto, result.ConvertToCustomValidationError());
            }
        }
    }
}