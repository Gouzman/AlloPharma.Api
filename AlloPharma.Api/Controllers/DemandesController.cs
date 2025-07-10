// ===================================================================================
// FICHIER : Controllers/DemandesController.cs (Nouveau fichier)
// DESCRIPTION : Contrôleur pour gérer la création et le suivi des demandes.
// ===================================================================================
using AlloPharma.Api.Data;
using AlloPharma.Api.Dtos;
using AlloPharma.Api.Hubs;
using AlloPharma.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace AlloPharma.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DemandesController : ControllerBase
    {
        private readonly AlloPharmaDbContext _context;
        private readonly IHubContext<DemandeHub> _demandeHubContext;

        public DemandesController(AlloPharmaDbContext context, IHubContext<DemandeHub> demandeHubContext)
        {
            _context = context;
            _demandeHubContext = demandeHubContext;
        }

        [HttpPost]
        public async Task<IActionResult> CreateDemande(CreateDemandeDto createDemandeDto)
        {
            // 1. Créer le point géographique du client
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var clientLocation = geometryFactory.CreatePoint(new Coordinate(createDemandeDto.Longitude, createDemandeDto.Latitude));

            // 2. Créer et sauvegarder la demande en base de données
            var nouvelleDemande = new Demande
            {
                ClientId = createDemandeDto.ClientId,
                Statut = DemandeStatut.EnCours,
                Items = createDemandeDto.Items.Select(item => new DemandeItem
                {
                    MedicamentId = item.MedicamentId,
                    Quantite = item.Quantite
                }).ToList()
            };
            _context.Demandes.Add(nouvelleDemande);
            await _context.SaveChangesAsync();


            // 3. Trouver les pharmacies à proximité (LA MAGIE DE POSTGIS)
            var rayonEnMetres = 10 * 1000; // 10 km
            var pharmaciesProches = await _context.Pharmacies
                .Where(p => p.IsVisibleSurCarte && p.Coordonnees.IsWithinDistance(clientLocation, rayonEnMetres))
                .ToListAsync();
            
            // 4. Notifier les pharmacies trouvées via SignalR
            if (pharmaciesProches.Any())
            {
                // Dans une vraie application, on ciblerait les pharmacies par leur ID de connexion.
                // Pour l'instant, on envoie à tout le monde.
                await _demandeHubContext.Clients.All.SendAsync("NouvelleDemandeRecue", new 
                { 
                    DemandeId = nouvelleDemande.Id, 
                    Message = $"Nouvelle demande à {pharmaciesProches.Count} pharmacies." 
                });
            }

            return Ok(new 
            { 
                Message = "Demande créée et envoyée aux pharmacies à proximité.",
                DemandeId = nouvelleDemande.Id,
                PharmaciesNotifiees = pharmaciesProches.Count 
            });
        }
    }
}
