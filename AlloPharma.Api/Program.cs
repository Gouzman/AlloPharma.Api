using AlloPharma.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// --- CONFIGURATION DES SERVICES ---

// 1. Ajouter la configuration pour la base de données PostgreSQL
builder.Services.AddDbContext<AlloPharmaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        // Activer le support de NetTopologySuite pour les requêtes géospatiales
        o => o.UseNetTopologySuite()));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// --- CONFIGURATION DU PIPELINE HTTP ---

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthentication(); // A activer quand le JWT sera configuré
app.UseAuthorization();

app.MapControllers();

app.Run();
