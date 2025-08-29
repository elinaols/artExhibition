// Gör det möjligt att använda klasser och funktioner från EntityFramework
using Microsoft.EntityFrameworkCore;
namespace examinationsuppgiftASP.Models
{
    public class ArtExhibitionContext : DbContext
    {
        // DbSet används för att utföra CRUD-operationer på tabellen för konstutställningar
        public DbSet<ArtExhibitions> ExhibitionList { get; set; }
        // DbSet används för att utföra CRUD-operationer på tabellen för konstnärer
        public DbSet<Artists> ArtistList { get; set; } 
        
        // Öppnar en ny instans av ArtExhibitionContext() för att hantera databasoperationer
        public ArtExhibitionContext()
        {
            // Kollar om databasen redan finns - om den inte finns skapas den automatiskt
            Database.EnsureCreated();
        }
        
        // Används för att konfigurera databasen
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Ställer in SQLite som databas, där datan lagras i filen "artExibition.db"
            optionsBuilder.UseSqlite("Data Source = artExhibition.db");
        }
    }
}
