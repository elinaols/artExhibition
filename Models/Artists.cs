namespace examinationsuppgiftASP.Models
{
    public class Artists
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        // Lista över konstutställningar kopplade till denna konstnär
        // Möjliggör fullständig koppling till ArtExhibitions-modellen via en främmande nyckel (ArtistId)
        public List<ArtExhibitions> ArtExhibition { get; set; }
    }
}
