using AdvertisementApp.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvidedServiceService _providedServiceService;

        // Constructor Injection ile servisimizi istiyoruz
        public HomeController(IProvidedServiceService providedServiceService)
        {
            _providedServiceService = providedServiceService;
        }

        // Metodu asenkron (async Task) yapıyoruz çünkü veritabanından veri bekleyeceğiz
        public async Task<IActionResult> Index()
        {
            // Servisimizden tüm hizmetleri çekiyoruz
            var response = await _providedServiceService.GetAllAsync();

         
            return View(response.Data); // View'e sadece veriyi gönderiyoruz, response'un tamamını değil
        }
    }
}