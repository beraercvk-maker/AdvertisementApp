using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.Threading.Tasks; 

namespace AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        
        // 1. AppUser servisini buraya ekledik!
        private readonly IAppUserService _appUserService; 

        // 2. Constructor içine AppUser servisini de istedik
        public AccountController(IGenderService genderService, IAppUserService appUserService)
        {
            _genderService = genderService;
            _appUserService = appUserService;
        }

        [HttpGet]
        public async Task<IActionResult> SignUp()
        {
            var response = await _genderService.GetAllAsync();
            var model = new UserCreateModel();
            model.Genders = new SelectList(response.Data, "Id", "Definition");
            return View(model);
        }
 
        [HttpPost]
public async Task<IActionResult> SignUp(UserCreateModel model)
{
    // 1. HTML formundan geri dönmeyecek olan özellikleri "Hata" olarak algılamaması için doğrulama dışı bırakıyoruz.
    ModelState.Remove("Genders"); 
    
    // Eğer UserCreateModel içinde "ConfirmPassword" varsa ama HTML formuna eklemediysen onu da buraya yazmalısın:
    // ModelState.Remove("ConfirmPassword");

    // 2. Artık IsValid true dönecektir
    if (ModelState.IsValid)
    {
        var dto = new AppUserCreateDto
        {
            Firstname = model.Firstname,
            Surname = model.Surname,
            Username = model.Username,
            Password = model.Password, // Şifreyi ileride hash'leyeceğiz (şifreleyeceğiz), şimdilik düz kaydediyoruz
            PhoneNumber = model.PhoneNumber,
            GenderId = model.GenderId,
            Email = model.Email
        };

        var response = await _appUserService.CreateAsync(dto);

        if (response.ResponseType == AdvertisementApp.Common.ResponseType.Success)
        {
            return RedirectToAction("HumanResources", "Home");
        }

        // Eğer Business katmanındaki kurallara takılırsa hataları yakala
        foreach (var error in response.ValidationErrors)
        {
            ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }

    // Hata varsa veya IsValid false ise listeyi tekrar doldur ve sayfayı göster
    var genderResponse = await _genderService.GetAllAsync();
    model.Genders = new SelectList(genderResponse.Data, "Id", "Definition");
    
    return View(model);
}
    }
}