using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.Common;
using AdvertisementApp.Dtos.AdvertisementUserDtos;
using AdvertisementApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        private readonly IAdvertisementService _advertisementService;
        private readonly IAdvertisementUserService _advertisementUserService;

        public AdvertisementController(IAdvertisementService advertisementService, IAdvertisementUserService advertisementUserService)
        {
            _advertisementService = advertisementService;
            _advertisementUserService = advertisementUserService;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _advertisementService.GetAllAsync();
            return View(response.Data);
        }

        [HttpGet]
        [Authorize(Roles = "Member,User")]
        public IActionResult Send(int AdvertisementId)
        {
            var model = new AdvertisementAppUserCreateModel
            {
                AdvertisementId = AdvertisementId
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Member,User")]
        public async Task<IActionResult> Send(AdvertisementAppUserCreateModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var userIdClaim = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userIdClaim) || !int.TryParse(userIdClaim, out var userId))
            {
                return RedirectToAction("SignIn", "Account");
            }

            var cvPath = "";
            if (model.CvFile != null && model.CvFile.Length > 0)
            {
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads", "cv");
                Directory.CreateDirectory(uploadsFolder);

                var fileName = $"{Guid.NewGuid()}_{model.CvFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await model.CvFile.CopyToAsync(stream);
                }

                cvPath = "/uploads/cv/" + fileName;
            }

            var dto = new AdvertisementUserCreateDto
            {
                AdvertisementId = model.AdvertisementId,
                AppUserId = userId,
                AdvertisementUserStatusId = model.AdvertisementAppUserStatusId,
                MilitaryStatusId = model.MilitaryStatusId,
                WorkExperience = model.WorkExperience,
                CvPath = cvPath,
                EndDate = model.EndDate
            };

            var response = await _advertisementUserService.CreateAsync(dto);
            if (response.ResponseType == ResponseType.Success)
            {
                return RedirectToAction(nameof(Index));
            }

            foreach (var error in response.ValidationErrors ?? new List<CustomValidationError>())
            {
                ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
            }

            return View(model);
        }
    }
}