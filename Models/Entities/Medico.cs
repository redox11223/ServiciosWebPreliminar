using System;

namespace preliminarServicios.Models.Entities;

public class Medico
{
    public int Id{get;set;}
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string NumeroLicencia{get;set;}
    public string? Telefono{get;set;}
    public int EspecialidadId{get;set;}
}
