// ===================================================================================
// FICHIER : Hubs/DemandeHub.cs (Nouveau fichier)
// DESCRIPTION : Hub SignalR pour la communication en temps réel.
// ===================================================================================
using Microsoft.AspNetCore.SignalR;

namespace AlloPharma.Api.Hubs
{
    // Ce hub sera utilisé pour envoyer des alertes aux pharmacies.
    public class DemandeHub : Hub
    {
        // Une pharmacie se connectera à ce hub après s'être authentifiée.
        // On pourrait ajouter une logique pour joindre des groupes (par exemple, par ville)
        // pour optimiser la diffusion des messages.
        public async Task AnnoncerNouvelleDemande(string message)
        {
            // Cette méthode peut être appelée par le serveur pour notifier tous les clients connectés.
            await Clients.All.SendAsync("NouvelleDemandeRecue", message);
        }
    }
}
