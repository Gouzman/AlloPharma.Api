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
        // Constructeur requis pour l'injection de dépendances.
        // Il reçoit les options (comme la chaîne de connexion) depuis Program.cs
        // et les passe à la classe de base DbContext.
        public AlloPharmaDbContext(DbContextOptions<AlloPharmaDbContext> options) : base(options)
        {
        }

        public DbSet<Pharmacy> Pharmacies { get; set; }
        public DbSet<Medicament> Medicaments { get; set; }
        public DbSet<Demande> Demandes { get; set; }
        public DbSet<DemandeItem> DemandeItems { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder); // Important d'appeler la méthode de base

            // Configuration pour PostGIS et l'unicité du téléphone
            modelBuilder.HasPostgresExtension("postgis");
            modelBuilder.Entity<Pharmacy>()
                .HasIndex(p => p.Telephone)
                .IsUnique();
        }
    }
}
