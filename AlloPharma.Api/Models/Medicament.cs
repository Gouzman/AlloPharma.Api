// ===================================================================================
// FICHIER : Models/Medicament.cs (Nouveau fichier)
// DESCRIPTION : Modèle simple pour un médicament.
// ===================================================================================

using System.ComponentModel.DataAnnotations;

namespace AlloPharma.Api.Models
{
    public class Medicament
    {
        public Guid Id { get; set; }
        [Required]
        public string NomComplet { get; set; } = string.Empty;
        // D'autres propriétés comme DCI, Forme, Dosage peuvent être ajoutées plus tard.
    }
}