using AdvertisementApp.Dtos.AppRoleDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AppRoleUpdateDtoValidator : AbstractValidator<AppRoleUpdateDto>
    {
        public AppRoleUpdateDtoValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Rol adı boş bırakılamaz.");
        }
    }
}