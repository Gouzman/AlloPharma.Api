// ===================================================================================
// FICHIER : Dtos/PharmacyLoginDto.cs
// DESCRIPTION : DTO pour la connexion.
// ===================================================================================
using System.ComponentModel.DataAnnotations;

namespace AlloPharma.Api.Dtos
{
    public class PharmacyLoginDto
    {
        [Required]
        public string Telephone { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}