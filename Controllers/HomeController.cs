// Ger möjlighet att använda klasser från modellerna utan att behöva skriva den fullständiga sökvägen varje gång
using examinationsuppgiftASP.Models;
// Gör det möjligt att använda klasser och funktioner från ASP.NET Core MVC
using Microsoft.AspNetCore.Mvc;

namespace examinationsuppgiftASP.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}