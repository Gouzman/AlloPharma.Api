// ===================================================================================
// FICHIER : Data/AlloPharmaDbContext.cs
// DESCRIPTION : Le contexte de base de données pour Entity Framework Core.
//               Il fait le lien entre notre code C# et la base de données PostgreSQL.
// ===================================================================================
using AlloPharma.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace AlloPharma.Api.Data
{
    public class AlloPharmaDbContext : DbContext
    {
        public AlloPharmaDbContext(DbContextOptions<AlloPharmaDbContext> options) : base(options)
        {
        }

        public DbSet<Pharmacy> Pharmacies { get; set; }
        // Ajoutez ici d'autres DbSet pour les Clients, Medicaments, etc. dans les futurs sprints

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Indique à PostgreSQL d'utiliser l'extension PostGIS pour les fonctions géospatiales
            modelBuilder.HasPostgresExtension("postgis");

            // S'assurer que le numéro de téléphone est unique dans la base de données
            modelBuilder.Entity<Pharmacy>()
                .HasIndex(p => p.Telephone)
                .IsUnique();
        }
    }
}
