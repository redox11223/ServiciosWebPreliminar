namespace preliminarServicios.Models.Dtos;

public record class HorarioMedicoDto(
    int Id,
    MedicoResumenDto Medico,
    DayOfWeek DiaSemana,
    TimeSpan HoraInicio,
    TimeSpan HoraFin
)
{
    public string DiaSemanaNombre=> DiaSemana switch
    {
        DayOfWeek.Monday => "Lunes",
        DayOfWeek.Tuesday => "Martes",
        DayOfWeek.Wednesday => "Miércoles",
        DayOfWeek.Thursday => "Jueves",
        DayOfWeek.Friday => "Viernes",
        DayOfWeek.Saturday => "Sábado",
        DayOfWeek.Sunday => "Domingo",
        _ => ""
    };
};
