using System.ComponentModel.DataAnnotations;

namespace preliminarServicios.Models.Dtos;

public record class CreateHorarioMedicoDto(
    [Required(ErrorMessage = "El ID del médico es obligatorio.")]
    [Range(1, int.MaxValue, ErrorMessage = "El ID del médico debe ser un número positivo.")]
    int MedicoId,

    [Required(ErrorMessage = "El día de la semana es obligatorio.")]
    DayOfWeek DiaSemana,
    
    [Required(ErrorMessage = "La hora de inicio es obligatoria.")]
    TimeSpan HoraInicio,
    
    [Required(ErrorMessage = "La hora de fin es obligatoria.")]
    TimeSpan HoraFin,

    [Range(5, 120, ErrorMessage = "La duración de la cita debe ser entre 5 y 120 minutos.")]
    int DuracionCita = 30
):IValidatableObject
{
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (HoraFin <= HoraInicio)
        {
            yield return new ValidationResult("La hora de fin debe ser mayor que la hora de inicio.", [nameof(HoraFin)]);
        }

        if ((HoraFin-HoraInicio).Minutes < DuracionCita)
        {
            yield return new ValidationResult("El rango de horario debe permitir al menos una cita.", [nameof(DuracionCita),nameof(HoraFin)]);
        }
        if(HoraInicio < TimeSpan.FromHours(6) || HoraFin > TimeSpan.FromHours(22))
        {
            yield return new ValidationResult("El horario debe estar entre las 6:00 y las 22:00.", [nameof(HoraInicio), nameof(HoraFin)]);
        }
    }
}
