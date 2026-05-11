using AdvertisementApp.Dtos.GenderDtos;
using FluentValidation;

namespace AdvertisementApp.Business.ValidationRules.FluentValidation
{
    public class GenderUpdateDtoValidator : AbstractValidator<GenderUpdateDto>
    {
        public GenderUpdateDtoValidator()
        {
            // Güncelleme işleminde Id kesinlikle olmalı
            RuleFor(x => x.Id)
                .NotEmpty();

            // Definition (Cinsiyet Adı) boş olamaz
            RuleFor(x => x.Definition)
                .NotEmpty().WithMessage("Cinsiyet tanımı boş geçilemez.");
        }
    }
}