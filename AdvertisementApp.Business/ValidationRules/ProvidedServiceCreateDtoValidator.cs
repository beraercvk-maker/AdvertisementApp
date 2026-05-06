using AdvertisementApp.Dtos;
using AdvertisementApp.Dtos.ProvidedServiceDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules
{
    public class ProvidedServiceCreateDtoValidator : AbstractValidator<ProvidedServiceCreateDto>
    {
        public ProvidedServiceCreateDtoValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Hizmet başlığı boş bırakılamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Hizmet açıklaması boş bırakılamaz.");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Lütfen bir resim yükleyin veya resim yolu belirtin.");
        }
    }
}