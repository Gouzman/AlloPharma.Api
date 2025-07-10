using AlloPharma.Api.Data;
using AlloPharma.Api.Hubs; // Ajouter ce using
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// ... (services existants)
builder.Services.AddDbContext<AlloPharmaDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
        o => o.UseNetTopologySuite()));

builder.Services.AddControllers();

// 2. Ajouter SignalR aux services
builder.Services.AddSignalR();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// 3. Mapper l'endpoint pour notre Hub SignalR
app.MapHub<DemandeHub>("/demandeHub");

app.Run();
