using System;

namespace preliminarServicios.Models.Entities;

public class HorarioMedico:EntidadBase
{
    public int MedicoId { get; set; }
    public required Medico Medico { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
}
