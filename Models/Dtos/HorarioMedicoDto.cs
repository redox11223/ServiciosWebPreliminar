namespace preliminarServicios.Models.Dtos;

public record class HorarioMedicoDto(
    int Id,
    MedicoResumenDto Medico,
    DayOfWeek DiaSemana,
    TimeSpan HoraInicio,
    TimeSpan HoraFin,
    int DuracionCita
);
