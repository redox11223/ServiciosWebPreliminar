using System;

namespace preliminarServicios.Models.Entities;

public class HorarioMedico
{
    public int Id { get; set; }
    public int MedicoId { get; set; }
    public DayOfWeek DiaSemana { get; set; }
    public TimeSpan HoraInicio { get; set; }
    public TimeSpan HoraFin { get; set; }
    public int DuracionCita{get;set;} = 30; // Duración en minutos  
}
