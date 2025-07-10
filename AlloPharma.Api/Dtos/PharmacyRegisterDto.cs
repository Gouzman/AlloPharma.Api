// ===================================================================================
// FICHIER : Dtos/PharmacyRegisterDto.cs
// DESCRIPTION : DTO (Data Transfer Object) pour l'inscription.
//               C'est l'objet que l'API s'attend à recevoir lors d'une inscription.
// ===================================================================================
using System.ComponentModel.DataAnnotations;

namespace AlloPharma.Api.Dtos
{
    public class PharmacyRegisterDto
    {
        [Required]
        public string NomPharmacie { get; set; } = string.Empty;
        
        [Required]
        public string Telephone { get; set; } = string.Empty;
        
        [Required]
        [MinLength(6)]
        public string Password { get; set; } = string.Empty;

        // Les coordonnées seront envoyées sous forme de latitude et longitude
        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Longitude { get; set; }

        public string? NomPharmacien { get; set; }
        public string? AdresseTexte { get; set; }
        public string? Commune { get; set; }
        public string? Quartier { get; set; }
    }
}
