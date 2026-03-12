using System;

namespace preliminarServicios.Models.Entities;

public class Paciente : EntidadBase
{
    public required string Nombre{get;set;}
    public required string Apellido{get;set;}
    public required string Dni{get;set;}
    public required string Email{get;set;}
    public string? Telefono{get;set;}
    public DateOnly FechaNacimiento{get;set;}
}
