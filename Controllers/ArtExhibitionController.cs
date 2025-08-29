// Ger möjlighet att använda klasser från modellerna utan att behöva skriva den fullständiga sökvägen varje gång
using examinationsuppgiftASP.Models;
// Gör det möjligt att använda klasser och funktioner från ASP.NET Core MVC
using Microsoft.AspNetCore.Mvc;
// Gör det möjligt att använda klasser och funktioner från EntityFramework
using Microsoft.EntityFrameworkCore;
// Hjälper till för att kunna använda klasser/funktioner relaterade till auktorisering
using Microsoft.AspNetCore.Authorization;
// Läggs till när man vill använda ViewBag för att skicka data till en vy. Hjälper till med rendering av HTML-element (skapa visuell presentation av data)
using Microsoft.AspNetCore.Mvc.Rendering;   

namespace examinationsuppgiftASP.Controllers
{
    // ArtExhibitionController ärver funktioner/egenskaper från Controller för att kunna hantera HTTP-förfrågningar och svar
    public class ArtExhibitionController : Controller
    {
        public IActionResult Index()
        {
            // Using ger databaskoppling och stänger kopplingen automatiskt efter den använts
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Anropar tabellen ExhibitionList i databasen för att hämta en lista av den. Include()-metoden inkluderar objektet Artist till listan
                // ToList() hämtar datan från databasen och gör en lista
                List<ArtExhibitions> ArtExhibitionList = db.ExhibitionList.Include(a => a.Artist).ToList();
                return View(ArtExhibitionList);
            }
        }
        public IActionResult Details(int Id)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext()) {
                // Find-metoden hjälper till att hämta rätt entitet från databasen (rätt rad) genom primärnyckeln ID
                ArtExhibitions chosenArtExhibition = db.ExhibitionList.Find(Id);
            
                return View(chosenArtExhibition);
            }
        }
        // Begränsar åtkomst för obehöriga, endast användare som är inloggade kan komma åt denna vy
        [Authorize]
        public IActionResult Create()
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // ViewBag lagrar värden som kan skickas till vyn. Värdet till ViewBag matchar kolumnen i databasen
                // SelectList skapar en dropdown med data från ArtistList, där ToList-metoden samlar ihop datan i en lista
                ViewBag.ArtistId = new SelectList(db.ArtistList.ToList(), "Id", "FirstName");
            }
                return View();
        }
        // Metoden HttpPost tar emot data från formuläret (efter knapptryck) och används för att skapa eller uppdatera en post i databasen
        [HttpPost]
        // Inparametrarna representerar de skickade värdena från formuläret
        public IActionResult Create(ArtExhibitions newExhibition)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Add()-metoden lägger till newExhibition i tabellen ExhibitionList i databasen
                db.ExhibitionList.Add(newExhibition);
                // Sparar detta i databasen med SaveChanges()-metoden
                db.SaveChanges();
            }
                // RedirectToAction hjälper till att ta upp listan i vyn igen för att användaren ska se att en konstutställning lagts till
                return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Edit(int Id) 
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Find-metoden hjälper till att hämta rätt entitet från databasen (rätt rad) genom primärnyckeln ID
                ArtExhibitions changeExhibition = db.ExhibitionList.Find(Id);
                // ViewBag lagrar värden som kan skickas till vyn. Detta värde matchar kolumnen i databasen
                // new SelectList() skapar en dropdown där ToList() samlar värden från ArtistList till en lista
                ViewBag.ArtistId = new SelectList(db.ArtistList.ToList(), "Id", "FirstName");
                return View(changeExhibition);
            } 
        }
        [HttpPost]
        public IActionResult Edit(ArtExhibitions changeExhibition)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Update()-metoden uppdaterar databasen med värdena från inparametern changeExhibition
                db.Update(changeExhibition);
                // SaveChanges() sparar de uppdaterade värdena i databasen
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
        [Authorize]
        public IActionResult Delete(int Id)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Find-metoden hjälper till att hämta rätt entitet från databasen (rätt rad) genom primärnyckeln ID
                ArtExhibitions removeExhibition = db.ExhibitionList.Find(Id);
                return View(removeExhibition);
            }
        }
        [HttpPost]
        public IActionResult Delete(ArtExhibitions removeExhibition)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Remove() tar bort den angivna entiteten, removeExhibition, från databasen
                db.Remove(removeExhibition);
                // SaveChanges() sparar alla ändringar, inklusive borttagningen, i databasen
                db.SaveChanges();
                return RedirectToAction("Index");
            }
        }
    }
}