using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common.Enums;
using AdvertisementApp.Dtos.AppUserDtos;
using AdvertisementApp.UI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering; 
using System.Threading.Tasks; 
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using AutoMapper; // Mapper için gerekli

namespace AdvertisementApp.UI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IGenderService _genderService;
        
        // 1. AppUser servisini buraya ekledik!
        private readonly IAppUserService _appUserService; 

        private readonly IMapper _mapper; // AutoMapper için gerekli

        // 2. Constructor içine AppUser servisini de istedik
        public AccountController(IGenderService genderService, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _appUserService = appUserService;
            _mapper = mapper;
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
    
    // 2. Artık IsValid true dönecektir
    if (ModelState.IsValid)
    {
        var dto = new AppUserCreateDto
        {
            Firstname = model.Firstname,
            Surname = model.Surname,
            Username = model.Username,
            Password = AdvertisementApp.Common.Helpers.PasswordHelper.HashPassword(model.Password),
            PhoneNumber = model.PhoneNumber,
            GenderId = model.GenderId,
            Email = model.Email
        };

        // İŞTE DEĞİŞEN SATIR BURASI: Artık rol ID'si ile birlikte gönderiyoruz!
        var response = await _appUserService.CreateWithRoleAsync(dto, RoleType.Member.GetHashCode()); // RoleType enum'undan Member'ı seçiyoruz ve int değerini gönderiyoruz.

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
#region Giriş Yap (SignIn) İşlemleri

[HttpGet]
public IActionResult SignIn()
{
    // Ekrana sadece boş giriş formunu (View) gönderir
    return View();
}

[HttpPost]
public async Task<IActionResult> SignIn(UserLoginModel model)
{
    if (ModelState.IsValid)
    {
        // 1. Kargo kutusunu (DTO) hazırla
        var dto = _mapper.Map<AppUserLoginDto>(model);

        // 2. Kullanıcı adı ve şifre doğru mu?
        var response = await _appUserService.CheckUserAsync(dto);

        if (response.ResponseType == AdvertisementApp.Common.ResponseType.Success)
        {
            // --- EKLENEN YENİ KISIM: ROLLERİ ÇEKİYORUZ ---
            // Başarılı giriş yapan kullanıcının Id'si ile rollerini veritabanından getir
            var roleResponse = await _appUserService.GetRolesByUserIdAsync(response.Data.Id);

            // Temel yaka kartı bilgileri (Şimdilik sadece Id var)
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Data.Id.ToString())
            };

            // Eğer kullanıcının rolleri başarıyla geldiyse, her birini yaka kartına "Rol" olarak ekle!
            if (roleResponse.ResponseType == AdvertisementApp.Common.ResponseType.Success)
            {
                foreach (var role in roleResponse.Data)
                {
                    // İşte sihri yapan kod bu! Sistemin [Authorize(Roles="Admin")] özelliğini tetikleyen yer.
                    claims.Add(new Claim(ClaimTypes.Role, role.Definition)); 
                }
            }
            // ---------------------------------------------

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = model.RememberMe 
            };

            // Kapıyı aç, yaka kartını ver ve içeri al!
            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return RedirectToAction("Index", "Home");
        }

        // Kullanıcı veya şifre yanlışsa
        ModelState.AddModelError("", response.Message); 
    }

    return View(model);
}

#endregion
        public async Task<IActionResult> LogOut()
        {
            // Kullanıcıyı sistemden at (Çıkış yap)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}