using AdvertisementApp.Dtos.AppUserDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AppUserLoginDtoValidator : AbstractValidator<AppUserLoginDto>
    {
        public AppUserLoginDtoValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Parola boş geçilemez.");
        }
    }
}