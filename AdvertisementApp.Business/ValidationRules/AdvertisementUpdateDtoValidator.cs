using AdvertisementApp.Dtos.AdvertisementDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules // Projendeki klasör yapısına göre namespace'i teyit et
{
    public class AdvertisementUpdateDtoValidator : AbstractValidator<AdvertisementUpdateDto>
    {
        public AdvertisementUpdateDtoValidator()
        {
            // Id (Kimlik) Kuralları
            // Güncelleme işlemi için geçerli bir Id gelmesi ŞARTTIR.
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Güncellenecek ilanın kimlik bilgisi (Id) boş olamaz.")
                .GreaterThan(0).WithMessage("Geçersiz bir ilan kimliği gönderildi.");

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