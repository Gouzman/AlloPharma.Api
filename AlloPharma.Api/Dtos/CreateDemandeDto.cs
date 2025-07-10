// ===================================================================================
// FICHIER : Dtos/CreateDemandeDto.cs (Nouveau fichier)
// DESCRIPTION : DTO pour la création d'une nouvelle demande.
// ===================================================================================
using System.ComponentModel.DataAnnotations;

namespace AlloPharma.Api.Dtos
{
    public class CreateDemandeDto
    {
        [Required]
        public Guid ClientId { get; set; } // L'ID du client qui fait la demande

        [Required]
        public double Latitude { get; set; } // Position actuelle du client

        [Required]
        public double Longitude { get; set; } // Position actuelle du client

        [Required]
        [MinLength(1)]
        public List<DemandeItemDto> Items { get; set; } = new();
    }

    public class DemandeItemDto
    {
        [Required]
        public Guid MedicamentId { get; set; }
        [Required]
        [Range(1, 100)]
        public int Quantite { get; set; }
    }
}