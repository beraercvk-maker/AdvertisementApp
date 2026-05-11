using AdvertisementApp.Dtos.AdvertisementDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules // Projendeki klasör adına göre namespace'i ayarlayabilirsin
{
    // AdvertisementCreateDto sınıfı için kuralları barındırır
    public class AdvertisementCreateDtoValidator : AbstractValidator<AdvertisementCreateDto>
    {
        public AdvertisementCreateDtoValidator()
        {
            // Title (Başlık) Kuralları
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("İlan başlığı boş bırakılamaz.")
                .NotNull().WithMessage("İlan başlığı zorunludur.")
                .MaximumLength(200).WithMessage("İlan başlığı en fazla 200 karakter olabilir.");

            // Description (Açıklama) Kuralları
            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("İlan açıklaması boş bırakılamaz.")
                .NotNull().WithMessage("İlan açıklaması zorunludur.");
        }
    }
}