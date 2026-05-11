using AdvertisementApp.Common;
using FluentValidation.Results; // FluentValidation'ın ValidationResult nesnesi için
using System.Collections.Generic;

namespace AdvertisementApp.Business.Extensions
{
    // Extension (Eklenti) sınıfları ve içindeki metotlar her zaman "static" olmak zorundadır.
    public static class ValidationResultExtension
    {
        // "this ValidationResult validationResult" kısmı bu metodun bir eklenti olduğunu belirtir.
        public static List<CustomValidationError> ConvertToCustomValidationError(this FluentValidation.Results.ValidationResult validationResult)
        {
            List<CustomValidationError> errors = new ();

            
            foreach (var error in validationResult.Errors)
            {
                errors.Add(new CustomValidationError
                {
                    ErrorMessage = error.ErrorMessage,
                    PropertyName = error.PropertyName
                });
            }
            
            return errors;
        }
    }
}




//FluentValidation'ın karmaşık hata listesini alıp, bizim kendi anladığımız o sade CustomValidationError listesine çevirmek. Yani bu dosya sadece "Hata Çevirmeni" olarak çalışıyor.