// ===================================================================================
// FICHIER : Models/Demande.cs (Nouveau fichier)
// DESCRIPTION : Modèle pour une demande initiée par un client.
// ===================================================================================
namespace AlloPharma.Api.Models
{
    public enum DemandeStatut
    {
        EnCours,
        Terminee,
        Expiree
    }

    public class Demande
    {
        public Guid Id { get; set; }
        public Guid ClientId { get; set; } // Pour l'instant, un simple Guid. Sera lié au modèle Client.
        public DateTime DateCreation { get; set; } = DateTime.UtcNow;
        public DemandeStatut Statut { get; set; } = DemandeStatut.EnCours;
        public int RayonRechercheKm { get; set; } = 10;
        
        // Navigation property
        public ICollection<DemandeItem> Items { get; set; } = new List<DemandeItem>();
    }

    public class DemandeItem
    {
        public Guid Id { get; set; }
        public Guid DemandeId { get; set; }
        public Guid MedicamentId { get; set; }
        public int Quantite { get; set; }
    }
}
