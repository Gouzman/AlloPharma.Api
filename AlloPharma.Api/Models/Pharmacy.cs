// ===================================================================================
// FICHIER : Models/Pharmacy.cs
// DESCRIPTION : Le modèle de données pour une pharmacie.
// ===================================================================================
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries; // Requis pour les coordonnées GPS

namespace AlloPharma.Api.Models
{
    public class Pharmacy
    {
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string   NomPharmacie { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? NomPharmacien { get; set; }

        public string? AdresseTexte { get; set; }
        public string? Commune { get; set; }
        public string? Quartier { get; set; }

        // C'est ici que PostGIS stockera les coordonnées précises.
        // Le type 'Point' vient du paquet NetTopologySuite.
        [Required]
        public Point Coordonnees { get; set; } = new Point(0, 0);

        [Required]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public bool IsVisibleSurCarte { get; set; } = true;
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
    }
}

