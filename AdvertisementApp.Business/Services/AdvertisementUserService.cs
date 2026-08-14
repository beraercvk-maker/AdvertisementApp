using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos.AdvertisementUserDtos;
using AdvertisementApp.Entities;

namespace AdvertisementApp.Business.Services
{
    public class AdvertisementUserService : IAdvertisementUserService
    {
        private readonly IUow _uow;

        public AdvertisementUserService(IUow uow)
        {
            _uow = uow;
        }

        public async Task<IResponse<AdvertisementUserCreateDto>> CreateAsync(AdvertisementUserCreateDto dto)
        {
            if (dto == null)
            {
                return new Response<AdvertisementUserCreateDto>(ResponseType.ValidationError, new AdvertisementUserCreateDto(), new List<CustomValidationError>
                {
                    new CustomValidationError { PropertyName = "General", ErrorMessage = "Başvuru bilgileri boş olamaz." }
                });
            }

            if (dto.AdvertisementId <= 0)
            {
                return new Response<AdvertisementUserCreateDto>(ResponseType.ValidationError, dto, new List<CustomValidationError>
                {
                    new CustomValidationError { PropertyName = nameof(dto.AdvertisementId), ErrorMessage = "İlan seçimi zorunludur." }
                });
            }

            if (dto.AppUserId <= 0)
            {
                return new Response<AdvertisementUserCreateDto>(ResponseType.ValidationError, dto, new List<CustomValidationError>
                {
                    new CustomValidationError { PropertyName = nameof(dto.AppUserId), ErrorMessage = "Kullanıcı bilgisi bulunamadı." }
                });
            }

            if (string.IsNullOrWhiteSpace(dto.CvPath))
            {
                return new Response<AdvertisementUserCreateDto>(ResponseType.ValidationError, dto, new List<CustomValidationError>
                {
                    new CustomValidationError { PropertyName = nameof(dto.CvPath), ErrorMessage = "CV dosyası yüklenmelidir." }
                });
            }

            var entity = new AdvertisementUser
            {
                AdvertisementId = dto.AdvertisementId,
                AppUserId = dto.AppUserId,
                AdvertisementUserStatusId = dto.AdvertisementUserStatusId,
                MilitaryStatusId = dto.MilitaryStatusId,
                WorkExperience = dto.WorkExperience,
                CvPath = dto.CvPath,
                EndDate = dto.EndDate ?? DateTime.MinValue
            };

            await _uow.GetRepository<AdvertisementUser>().CreateAsync(entity);
            await _uow.SaveChangesAsync();

            return new Response<AdvertisementUserCreateDto>(ResponseType.Success, dto);
        }
    }
}
