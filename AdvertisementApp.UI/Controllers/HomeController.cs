using AdvertisementApp.Business.Interfaces;
using AdvertisementApp.UI.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AdvertisementApp.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvidedServiceService _providedServiceService;
        private readonly IAdvertisementService _advertisementService;

        // Constructor Injection ile servisimizi istiyoruz
        public HomeController(IProvidedServiceService providedServiceService, IAdvertisementService advertisementService)
        {
            _providedServiceService = providedServiceService;
            _advertisementService = advertisementService;
        }

        // Metodu asenkron (async Task) yapıyoruz çünkü veritabanından veri bekleyeceğiz
        public async Task<IActionResult> Index()
        {
            // Servisimizden tüm hizmetleri çekiyoruz
            var response = await _providedServiceService.GetAllAsync();

         
            return View(response.Data); // View'e sadece Data kısmını gönderiyoruz, böylece ViewModel'imiz daha temiz olur
        }

        public async Task<IActionResult> Advertisements()
        {
            var response = await _advertisementService.GetActivesAsync();
            return this.ResponseView(response); // ResponseView, bizim özel bir ViewResult'ımız. İçinde hem status kontrolü yapıyor hem de uygun view'e yönlendiriyor.
        }


 public async Task<IActionResult> HumanResources()
{
    var response = await _advertisementService.GetActivesAsync();
    
    // SİGORTA KODU: Eğer arka plandan Data 'null' gelirse, sayfayı patlatmak yerine ona boş bir liste (new List) ver.
    var modelData = response.Data ?? new List<AdvertisementApp.Dtos.AdvertisementDtos.AdvertisementListDto>();
    
    return View(modelData); 
}

       



    }
}