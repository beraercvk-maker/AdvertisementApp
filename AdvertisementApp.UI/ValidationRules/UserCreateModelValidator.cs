using AdvertisementApp.UI.Models;
using FluentValidation;

namespace AdvertisementApp.UI.ValidationRules
{
    public class UserCreateModelValidator : AbstractValidator<UserCreateModel>
    {
        public UserCreateModelValidator()
        {
            // Ad ve Soyad Kuralları
            RuleFor(x => x.Firstname)
                .NotEmpty().WithMessage("Ad alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Ad en fazla 50 karakter olabilir.");

            RuleFor(x => x.Surname)
                .NotEmpty().WithMessage("Soyad alanı boş bırakılamaz.")
                .MaximumLength(50).WithMessage("Soyad en fazla 50 karakter olabilir.");

            // Kullanıcı Adı Kuralları
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş bırakılamaz.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.")
                .MaximumLength(20).WithMessage("Kullanıcı adı en fazla 20 karakter olabilir.");

            // E-Posta Kuralları (Hem boş olmamasını hem de @ işaretli mail formatında olmasını denetler)
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-Posta adresi boş bırakılamaz.")
                .EmailAddress().WithMessage("Lütfen geçerli bir e-posta adresi giriniz.");

            // Şifre Kuralları
            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre boş bırakılamaz.")
                .MinimumLength(6).WithMessage("Şifre güvenliğiniz için en az 6 karakter olmalıdır.");

            // Telefon Kuralları
            RuleFor(x => x.PhoneNumber)
                .NotEmpty().WithMessage("Telefon numarası boş bırakılamaz.");

            // Cinsiyet Seçimi (Dropdown'dan boş değer "Seçiniz" gelirse hata verir)
            RuleFor(x => x.GenderId)
                .NotEmpty().WithMessage("Lütfen bir cinsiyet seçiniz.");
        }
    }
}