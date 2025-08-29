// Hjälper till för att kunna använda klasser/funktioner relaterade till auktorisering
using Microsoft.AspNetCore.Authorization;
// Ger möjlighet att använda klasser från modellerna utan att behöva skriva den fullständiga sökvägen varje gång
using examinationsuppgiftASP.Models;
// Gör det möjligt att använda klasser och funktioner från ASP.NET Core MVC
using Microsoft.AspNetCore.Mvc;

namespace examinationsuppgiftASP.Controllers
{
    public class ArtistController : Controller
    {
        public IActionResult Index()
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Deklarerar en lista av objekt av typen Artists. ToList() samlar ihop all data i en lista
                List<Artists> ArtistList = db.ArtistList.ToList();
                return View(ArtistList);
            }
        }
        // Begränsar åtkomst för obehöriga, endast användare som är inloggade kan komma åt denna vy
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }
        // Metoden HttpPost tar emot data från formuläret och används för att skapa eller uppdatera en post i databasen
        [HttpPost]
        public IActionResult Create(Artists newArtist)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Add()-metoden lägger till newArtist i tabellen ArtistList i databasen
                db.ArtistList.Add(newArtist);
                // Sparar detta i databasen med metoden SaveChanges()
                db.SaveChanges();
            }
            // RedirectToAction hjälper till att ta upp listan i vyn igen för att användaren ska se att en konstnär lagts till
            return RedirectToAction("Index");
        }
        [Authorize]
        public IActionResult Edit(int Id)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Find-metoden hjälper till att hämta rätt entitet från databasen (rätt rad) genom primärnyckeln ID
                Artists changeArtist = db.ArtistList.Find(Id);
                return View(changeArtist);
            }
        }
        [HttpPost]
        public IActionResult Edit(Artists changeArtist)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Update()-metoden uppdaterar databasen med värdena från inparametern changeExhibition
                db.Update(changeArtist);
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
                Artists removeArtist = db.ArtistList.Find(Id);
                return View(removeArtist);
            }
        }
        [HttpPost]
        public IActionResult Delete(Artists removeArtist)
        {
            using (ArtExhibitionContext db = new ArtExhibitionContext())
            {
                // Remove() tar bort den angivna entiteten från databasen
                db.Remove(removeArtist);
                // SaveChanges() sparar alla ändringar, inklusive borttagningen, i databasen
                db.SaveChanges();
            }
                return RedirectToAction("Index");
        }
    }
}
