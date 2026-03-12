using System;

namespace preliminarServicios.Models.Entities;

public class Medico:EntidadBase
{
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string NumeroLicencia{get;set;}
    public string? Telefono{get;set;}
    public int EspecialidadId{get;set;}
    public int DuracionCita { get; set; } = 30; // Duración en minutos
}
