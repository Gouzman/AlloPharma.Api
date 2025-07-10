// ===================================================================================
// FICHIER : Controllers/AuthController.cs
// DESCRIPTION : Le contrôleur qui gère les routes /register et /login.
// ===================================================================================
using AlloPharma.Api.Data;
using AlloPharma.Api.Dtos;
using AlloPharma.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using NetTopologySuite.Geometries;

namespace AlloPharma.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AlloPharmaDbContext _context;

        public AuthController(AlloPharmaDbContext context)
        {
            _context = context;
        }

        [HttpPost("pharmacy/register")]
        public async Task<IActionResult> Register(PharmacyRegisterDto registerDto)
        {
            // Vérifier si une pharmacie avec ce numéro de téléphone existe déjà
            if (await _context.Pharmacies.AnyAsync(p => p.Telephone == registerDto.Telephone))
            {
                return BadRequest("Ce numéro de téléphone est déjà utilisé.");
            }

            // Créer le point géographique à partir de la latitude et longitude
            // SRID 4326 est la norme pour les coordonnées GPS (WGS 84)
            var geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            var location = geometryFactory.CreatePoint(new Coordinate(registerDto.Longitude, registerDto.Latitude));

            var pharmacy = new Pharmacy
            {
                NomPharmacie = registerDto.NomPharmacie,
                Telephone = registerDto.Telephone,
                // Hachage du mot de passe - NE JAMAIS STOCKER EN CLAIR
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(registerDto.Password),
                Coordonnees = location,
                NomPharmacien = registerDto.NomPharmacien,
                AdresseTexte = registerDto.AdresseTexte,
                Commune = registerDto.Commune,
                Quartier = registerDto.Quartier
            };

            _context.Pharmacies.Add(pharmacy);
            await _context.SaveChangesAsync();

            return Ok(new { Message = "Inscription réussie !" });
        }

        [HttpPost("pharmacy/login")]
        public async Task<IActionResult> Login(PharmacyLoginDto loginDto)
        {
            var pharmacy = await _context.Pharmacies.FirstOrDefaultAsync(p => p.Telephone == loginDto.Telephone);

            // Vérifier si la pharmacie existe et si le mot de passe est correct
            if (pharmacy == null || !BCrypt.Net.BCrypt.Verify(loginDto.Password, pharmacy.PasswordHash))
            {
                return Unauthorized("Numéro de téléphone ou mot de passe incorrect.");
            }

            // Si la connexion réussit, on génère un Token JWT (JSON Web Token)
            // La logique de génération du token sera ajoutée ici.
            // Pour l'instant, nous retournons un message de succès.
            // TODO: Générer et retourner un token JWT
            
            var token = "GENERATED_JWT_TOKEN_PLACEHOLDER"; // Placeholder

            return Ok(new { Message = "Connexion réussie !", Token = token });
        }
    }
}

