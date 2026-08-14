using AdvertisementApp.Dtos.AppRoleDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules
{
    public class AppRoleCreateDtoValidator : AbstractValidator<AppRoleCreateDto>
    {
        public AppRoleCreateDtoValidator()
        {
            RuleFor(x => x.Definition).NotEmpty().WithMessage("Rol adı boş bırakılamaz.");
        }
    }
}