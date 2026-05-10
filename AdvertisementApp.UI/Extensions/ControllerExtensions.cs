using AdvertisementApp.Common;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementApp.UI.Extensions
{
    public static class ControllerExtensions
    {
        // Videodaki tam yapı: IResponse<T> alan ve yönlendirme yapan metot
        public static IActionResult ResponseRedirectToAction<T>(this Controller controller, IResponse<T> response, string actionName)
        {
            // 1. Veri bulunamadı hatası geldiyse
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }

            // 2. Validasyon (Doğrulama) hatası geldiyse
            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    // Hataları ekrana basmak üzere ModelState'e ekliyoruz
                    controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                // Hatalı verilerle beraber formu (View) tekrar gösteriyoruz ki kullanıcı düzeltsin
                return controller.View(response.Data);
            }

            // 3. Her şey başarılıysa (Success), bizi istediğimiz sayfaya (actionName) uçur!
            return controller.RedirectToAction(actionName);
        }

        public static IActionResult ResponseView<T>(this Controller controller, IResponse<T> response)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }

            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            return controller.View(response.Data);
        }

        public static IActionResult ResponseRedirectToAction<T>(this Controller controller, IResponse<T> response, string actionName, string controllerName)
        {
            if (response.ResponseType == ResponseType.NotFound)
            {
                return controller.NotFound();
            }

            if (response.ResponseType == ResponseType.ValidationError)
            {
                foreach (var error in response.ValidationErrors)
                {
                    controller.ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return controller.View(response.Data);
            }

            return controller.RedirectToAction(actionName, controllerName);
        }
    }
}