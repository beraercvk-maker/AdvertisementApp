using AdvertisementApp.Dtos.GenderDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation
{
    public class GenderCreateDtoValidator : AbstractValidator<GenderCreateDto>
    {
        public GenderCreateDtoValidator()
        {
            // Definition (Cinsiyet Adı) boş olamaz
            RuleFor(x => x.Definition)
                .NotEmpty().WithMessage("Cinsiyet tanımı boş geçilemez.");
        }
    }
}