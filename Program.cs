using Microsoft.EntityFrameworkCore;
using preliminarServicios.Data;
using preliminarServicios.Services;
using Scalar.AspNetCore;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ClinicaDbContext>(options =>
    options.UseSqlServer(connectionString));

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddScoped<IPacienteService, PacienteService>();
builder.Services.AddScoped<IMedicoService, MedicoService>();
builder.Services.AddScoped<IEspecialidadService, EspecialidadService>();
builder.Services.AddScoped<ICitaService, CitaService>();
builder.Services.AddScoped<IHorarioMedicoService, HorarioMedicoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
        // AGREGA ESTO PARA LA INTERFAZ VISUAL:
    app.MapScalarApiReference(options => 
    {
        options.WithTitle("Mi Super API .NET 10")
               .WithTheme(ScalarTheme.DeepSpace) // Elige el tema que quieras
               .WithDefaultHttpClient(ScalarTarget.CSharp, ScalarClient.HttpClient);
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
