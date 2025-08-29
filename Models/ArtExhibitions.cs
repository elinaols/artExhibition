namespace examinationsuppgiftASP.Models
{
    // Representerar konstutställning
    public class ArtExhibitions
    {
        public int Id { get; set; }
        public string ExhibitionTitle { get; set; }
        public string Description { get; set; }
        // ID för konstnärer - används som främmande nyckel för att koppla konstutställningar till en specifik konstnär
        public int ArtistId { get; set; }
        // Objekt av konstnären som är kopplad till denna konstutställning. Skapar relation mellan ArtExhibitions och Artists
        public Artists Artist { get; set; }
    }
}
