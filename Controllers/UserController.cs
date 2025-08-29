// Ger möjlighet att använda klasser från modellerna utan att behöva skriva den fullständiga sökvägen varje gång
using examinationsuppgiftASP.Models;
// Hjälper till för att kunna använda klasser/funktioner relaterade till autentisering
using Microsoft.AspNetCore.Authentication;
// Gör det möjligt att använda cookie-autentisering som lagrar inloggningsinformation i cookies för att kontrollera om användaren är inloggad
using Microsoft.AspNetCore.Authentication.Cookies;
// Gör det möjligt att använda klasser och funktioner från ASP.NET Core MVC
using Microsoft.AspNetCore.Mvc;
// Används vid auktorisering och autentisering för att hantera användares identitetsinformation och rättigheter 
using System.Security.Claims;

namespace examinationsuppgiftASP.Controllers
{
    public class UserController : Controller
    {
        // Index-metoden visar startsidan och tar emot en eventuell återkommande URL som parameter
        public IActionResult Index(string returnURL = "")
        {
            // ViewData lagrar återkommande URL för att användas i vyn 
            ViewData["returnUrl"] = returnURL;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Users user, string returnURL = "")
        {
            // Checkar användarens inloggning
            bool validUser = CheckUser(user);
            if (validUser == true)
            {
                // Skapar en identitet för den inloggade användaren och lagrar användarinformation (namn och rättigheter)
                // som kan användas för autentisering och auktorisering av användare
                var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
                // Lägger till användarens namn som en claim (information om användaren)
                // Detta gör att vi kan identifiera användaren under autentisering och auktorisering
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Username));
                // Loggar in användare och sparar dennes identitetsinformation i en cookie så användaren förblir inloggad inför framtida inloggningsbegärningar.
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                // Om returnURL inte är tom, så ska användaren omdirigeras till den angivna URL:en
                if (returnURL != "")
                    return Redirect(returnURL);
                else
                    // Annars ska användaren omdirigeras till startsidan
                    return RedirectToAction("Index", "Home");
            }
            else
            {
                // Om inloggningen misslyckas kommer ett felmeddelande visas för användaren
                ViewBag.ErrorMessage = "Wrong username or password";
                // Lagrar returnURL för att kunna använda den senare
                @ViewData["returnUrl"] = returnURL;
                return View();
            }
        }
        private bool CheckUser(Users user)
        {
            // Checkar inloggningsuppgifter mot hårdkodade värden
            if (user.Username.ToUpper() == "ADMIN" && user.Password == "pwd")
                // Giltig inloggning
                return true;
            else
                // Misslyckad inloggning
                return false;
        }
        public async Task<IActionResult> SignOutUser()
        {
            // Loggar ut användaren genom att ta bort autentiseringstoken (användarens bekräftade identitet)
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            // Går tillbaka till startsidan
            return RedirectToAction("Index", "Home");
        }
    }
}