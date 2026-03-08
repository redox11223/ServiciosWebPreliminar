namespace preliminarServicios.Models.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateCitaDto(

    [Required(ErrorMessage = "El ID del paciente es obligatorio.")]
    int PacienteId,

    [Required(ErrorMessage = "El ID del médico es obligatorio.")]
    int MedicoId,

    [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser un número positivo.")]
    decimal Costo,

    [Required(ErrorMessage = "El motivo de la cita es obligatorio.")]
    string Motivo,

    [Required(ErrorMessage = "La fecha de inicio es obligatoria.")  ]
    DateTime FechaInicio,

    string? Observaciones
) : IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (FechaInicio < DateTime.Now)
        {
            yield return new ValidationResult("La fecha de inicio debe ser una fecha futura.", [nameof(FechaInicio)]);
        }
    }
}