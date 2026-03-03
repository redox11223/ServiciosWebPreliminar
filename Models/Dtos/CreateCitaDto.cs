namespace preliminarServicios.Models.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateCitaDto(
    
    [Required] 
    int PacienteId,
    
    [Required] 
    int MedicoId,
    
    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser un número positivo.")]
    decimal Costo,

    [Required]
    string Motivo,

    [Required]
    DateTime FechaInicio,
    
    [Required]
    DateTime FechaFin,
    string? Observaciones
);
