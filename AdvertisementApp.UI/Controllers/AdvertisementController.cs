using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.UI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class AdvertisementController : Controller
    {
        // 1. Veritabanından ilanları çekecek servisi (işçiyi) çağırıyoruz
        private readonly IAdvertisementService _advertisementService;

        public AdvertisementController(IAdvertisementService advertisementService)
        {
            _advertisementService = advertisementService;
        }

        // 2. İlanların Listelendiği Sayfa
        public async Task<IActionResult> Index()
        {
            // Tüm aktif ilanları veritabanından çek (GetAllAsync veya benzeri metodun)
            var response = await _advertisementService.GetAllAsync();
            
            // Çektiğin ilanları View'a kargo olarak gönder!
            return View(response.Data);
        }

        // 3. Başvuru Sayfası
        //[Authorize(Roles = "Member")]
        [HttpGet]
        public IActionResult Send(int AdvertisementId)
        {
            var model = new AdvertisementAppUserCreateModel
            {
                AdvertisementId = AdvertisementId
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult Send(AdvertisementAppUserCreateModel model)
        {
            return RedirectToAction(nameof(Index));
        }
    }
}