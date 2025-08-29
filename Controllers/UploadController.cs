using examinationsuppgiftASP.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace examinationsuppgiftASP.Controllers
{
    public class UploadController : Controller
    {
        // Hämtad från https://stackoverflow.com/questions/15989764/display-all-images-in-a-folder-in-mvc-with-a-foreach
        public IActionResult Index()
        {
            // Skapar en ny instans av modellen UploadFiles
            var fileList = new UploadFiles()
            {
                // Directory.EnumerateFiles hämtar alla filnamn från mappen 'wwwroot/uploads'
                Images = Directory.EnumerateFiles("wwwroot/uploads")
                // Select-metoden hjälper till att omvandla varje filnamn till en relativ sökväg
                // ToList()-metoden skapar en lista av sökvägarna som lagras i Images
                .Select(i => "/uploads/" + Path.GetFileName(i)).ToList()
            };
            // Returnerar en lista som innehåller sökväg för varje bild i mappen 'wwwroot/uploads'
            return View(fileList);
        }
        // Hämtad från https://www.thetechplatform.com/post/upload-file-to-a-folder-in-asp-net-core-mvc
        [Authorize]
        public async Task<IActionResult> Upload(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                // Om filen är tom eller saknas kommer följande meddelande returneras 
                return Content("File not selected");
            }

            // Path.Combine används för att kombinera flera strängar till en enda filväg
            // Directory.GetCurrentDirectory()-metoden hämtar den aktuella arbetskatalogens sökväg för att tillslut kunna skapa en fullständig sökväg där bilderna ska lagras
            // file.Filename ger namnet på filen som användaren valt att ladda upp
            var path = Path.Combine(
                        Directory.GetCurrentDirectory(), "wwwroot/uploads",
                        file.FileName);

            // Using hjälper till att stänga filströmmen korrekt efter den använts
            // Skapar en ny FileStream för att spara den nya uppladdade filen till den angvina sökvägen (path)
            // FileMode.Create anger att en ny fil ska skapas och läggas till (om filen redan finns skrivs den över direkt)
            using (var stream = new FileStream(path, FileMode.Create))
            {
                // Kopierar asynkront innehållet från den nya uppladdade filen till Filestream
                await file.CopyToAsync(stream);
            }
            // RedirectToAction hjälper till att ta upp listan i vyn igen för att användaren ska se den nya bilden som laddats upp
            return RedirectToAction("Index");
        }
    }
}