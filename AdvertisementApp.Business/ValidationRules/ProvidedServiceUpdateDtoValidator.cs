using AdvertisementApp.Dtos.ProvidedServiceDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules
{
    public class ProvidedServiceUpdateDtoValidator : AbstractValidator<ProvidedServiceUpdateDto>
    {
        public ProvidedServiceUpdateDtoValidator()
        {
            // Update için en önemli fark: ID zorunludur
            RuleFor(x => x.Id).NotEmpty().WithMessage("Güncellenecek kaydın kimliği (ID) bulunamadı.");

            RuleFor(x => x.Title).NotEmpty().WithMessage("Hizmet başlığı boş bırakılamaz.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Hizmet açıklaması boş bırakılamaz.");
            RuleFor(x => x.ImagePath).NotEmpty().WithMessage("Lütfen bir resim yükleyin veya resim yolu belirtin.");
        }
    }
}