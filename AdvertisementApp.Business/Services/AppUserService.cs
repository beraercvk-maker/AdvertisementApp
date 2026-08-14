using AdvertisementApp.Business.Extensions; // Validation hatalarını çevirmek için (CustomValidationError)
using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common; // Response tipleri için
using AdvertisementApp.DataAccess.Interfaces;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.Entities;
using AutoMapper;
using FluentValidation;
using System.Threading.Tasks;
using System.Collections.Generic; // (List<> kullanacağımız için eğer yoksa bunu da ekle)
using System.Linq;
using AdvertisementApp.Dtos.AppRoleDtos; // (.Any() metodunu kullanabilmek için)
namespace AdvertisementApp.Business.Services
{
    public class AppUserService : Service<AppUserCreateDto, AppUserUpdateDto, AppUserListDto, AppUser>, IAppUserService
    {
        private readonly IUow _uow;
        private readonly IMapper _mapper;
        private readonly IValidator<AppUserCreateDto> _createDtoValidator;
        private readonly IValidator<AppUserLoginDto> _loginDtoValidator;

        public AppUserService(IMapper mapper, IValidator<AppUserCreateDto> createDtoValidator, IValidator<AppUserUpdateDto> updateDtoValidator,IValidator<AppUserLoginDto> loginDtoValidator, IUow uow) 
            : base(mapper, createDtoValidator, updateDtoValidator, uow)
        {
            _uow = uow;
            _mapper = mapper;
            _createDtoValidator = createDtoValidator;
            _loginDtoValidator = loginDtoValidator;
            
        }

        // --- YENİ EKLENEN ÖZEL METOT ---
        public async Task<IResponse<AppUserCreateDto>> CreateWithRoleAsync(AppUserCreateDto dto, int roleId)
        {
            // 1. Validasyon Kurallarını Kontrol Et (Kapıdaki Güvenlik)
            var validationResult = _createDtoValidator.Validate(dto);
            if (validationResult.IsValid)
            {
                // 2. DTO'yu Veritabanı Entity'sine Çevir
                var user = _mapper.Map<AppUser>(dto);

                // 3. Kullanıcıyı Oluştur (Henüz SQL'e gitmedi, bellekte)
                await _uow.GetRepository<AppUser>().CreateAsync(user);

                // 4. Kullanıcıya Rolünü Ata (Henüz SQL'e gitmedi, bellekte)
                await _uow.GetRepository<AppUserRole>().CreateAsync(new AppUserRole
                {
                    AppUser = user,
                    AppRoleId = roleId
                });

                // 5. Unit of Work ile Tüm Değişiklikleri Tek Seferde SQL'e Yaz!
                await _uow.SaveChangesAsync();

                return new Response<AppUserCreateDto>(ResponseType.Success, dto);
            }

            // Validasyon hatası varsa hataları listele ve geri dön
            return new Response<AppUserCreateDto>(ResponseType.ValidationError, dto, validationResult.ConvertToCustomValidationError()); // Extension method ile FluentValidation hatalarını CustomValidationError listesine çeviriyoruz.
        }

        public async Task<IResponse<AppUserListDto>> CheckUserAsync(AppUserLoginDto dto)
{
    // 1. Önce DTO'yu kontrol et (Kullanıcı adı ve şifre boş mu gelmiş?)
    var validationResult = _loginDtoValidator.Validate(dto);
    
    if (validationResult.IsValid) // Eğer doğrulama kuralları sağlanıyorsa (örneğin, kullanıcı adı ve şifre boş değilse) devam et
    {
        // 2. Gelen şifreyi Hash'le ki veritabanındaki ile karşılaştırabilelim
        var hashedPassword = AdvertisementApp.Common.Helpers.PasswordHelper.HashPassword(dto.Password);

        // 3. Veritabanına sor: "Böyle bir kullanıcı adı ve bu şifreye sahip biri var mı?"
        var user = await _uow.GetRepository<AppUser>().GetByFilterAsync(x => x.UserName == dto.Username && x.Password == hashedPassword);

        if (user != null)
        {
            // Kullanıcı bulundu! Entity'i DTO'ya çevir ve başarılı olarak gönder.
            var appUserDto = _mapper.Map<AppUserListDto>(user);
            return new Response<AppUserListDto>(ResponseType.Success, appUserDto);
        }

        // Kullanıcı bulunamadı (Kullanıcı adı veya şifre yanlış)
        return new Response<AppUserListDto>(ResponseType.NotFound, "Kullanıcı adı veya şifre hatalı");
    }

    // Validasyon hatası varsa (Örn: şifreyi boş girdiyse) hataları döndür
return new Response<AppUserListDto>(ResponseType.ValidationError, new AppUserListDto(), validationResult.ConvertToCustomValidationError());
}

// --- KULLANICININ ROLLERİNİ GETİREN METOT ---
        public async Task<IResponse<List<AppRoleListDto>>> GetRolesByUserIdAsync(int userId)
        {
            // 1. Veritabanından Repository aracılığıyla Rolleri çekiyoruz.
            // "AppUserRoles tablosunda bu userId'ye sahip olan rolleri getir" diyoruz.
            var roles = await _uow.GetRepository<AppRole>()
                .GetAllAsync(x => x.UserRole.Any(ur => ur.AppUserId == userId));

            // 2. Eğer rol bulunamadıysa NotFound dönüyoruz
            if (roles == null || !roles.Any())
            {
                return new Response<List<AppRoleListDto>>(ResponseType.NotFound, "Kullanıcıya ait rol bulunamadı.");
            }

            // 3. Bulunan rolleri (Entity), DTO'ya (Kargo kutusuna) çeviriyoruz
            var dto = _mapper.Map<List<AppRoleListDto>>(roles);
            
            // 4. Başarılı olarak geriye döndürüyoruz
            return new Response<List<AppRoleListDto>>(ResponseType.Success, dto);
        }
  }
}