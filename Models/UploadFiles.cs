// Hämtad från https://stackoverflow.com/questions/15989764/display-all-images-in-a-folder-in-mvc-with-a-foreach
namespace examinationsuppgiftASP.Models
{
    public class UploadFiles
    {
        // IEnumerable<string> är en lista av strängar (filnamn för bilderna) som hjälper till att lagra sökvägar för bilder. 
        public IEnumerable<string> Images { get; set; }
    }
}