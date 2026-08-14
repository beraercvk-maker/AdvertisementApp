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
        private readonly IAppUserService _appUserService;
        private readonly IMapper _mapper;

        public AccountController(IGenderService genderService, IAppUserService appUserService, IMapper mapper)
        {
            _genderService = genderService;
            _appUserService = appUserService;
            _mapper = mapper;
        }

        private async Task SignInUserAsync(string username, string password, bool rememberMe)
        {
            var response = await _appUserService.CheckUserAsync(new AppUserLoginDto
            {
                Username = username,
                Password = password
            });

            if (response.ResponseType != AdvertisementApp.Common.ResponseType.Success)
            {
                throw new InvalidOperationException("Kullanıcı doğrulanamadı.");
            }

            var roleResponse = await _appUserService.GetRolesByUserIdAsync(response.Data.Id);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, response.Data.Id.ToString()),
                new Claim(ClaimTypes.Name, response.Data.Username)
            };

            if (roleResponse.ResponseType == AdvertisementApp.Common.ResponseType.Success)
            {
                foreach (var role in roleResponse.Data)
                {
                    claims.Add(new Claim(ClaimTypes.Role, role.Definition));

                    if (role.Definition == "User")
                    {
                        claims.Add(new Claim(ClaimTypes.Role, "Member"));
                    }
                }
            }

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);
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
            ModelState.Remove("Genders");

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

                var response = await _appUserService.CreateWithRoleAsync(dto, RoleType.Member.GetHashCode());

                if (response.ResponseType == AdvertisementApp.Common.ResponseType.Success)
                {
                    try
                    {
                        await SignInUserAsync(model.Username, model.Password, rememberMe: true);
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Hesap oluşturuldu fakat otomatik giriş sırasında bir hata oluştu.");
                    }
                }

                foreach (var error in response.ValidationErrors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }

            var genderResponse = await _genderService.GetAllAsync();
            model.Genders = new SelectList(genderResponse.Data, "Id", "Definition");

            return View(model);
        }

        #region Giriş Yap (SignIn) İşlemleri

        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dto = _mapper.Map<AppUserLoginDto>(model);
                var response = await _appUserService.CheckUserAsync(dto);

                if (response.ResponseType == AdvertisementApp.Common.ResponseType.Success)
                {
                    try
                    {
                        await SignInUserAsync(model.Username, model.Password, model.RememberMe);
                        return RedirectToAction("Index", "Home");
                    }
                    catch
                    {
                        ModelState.AddModelError(string.Empty, "Oturum açılırken bir hata oluştu.");
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, response.Message);
                }
            }

            return View(model);
        }

        #endregion

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }
    }
}