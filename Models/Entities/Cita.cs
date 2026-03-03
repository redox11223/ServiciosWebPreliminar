using System;
using preliminarServicios.Models.Enums;

namespace preliminarServicios.Models.Entities;

public class Cita
{
    public int Id{get;set;}
    public int PacienteId{get;set;}
    public int MedicoId{get;set;}
    public decimal Costo{get;set;}
    public required string Motivo{get;set;}
    public DateTime FechaInicio{get;set;}
    public DateTime FechaFin{get;set;}
    public string? Observaciones{get;set;}   
    public CitaEstado Estado{get;set;}
}
