using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface IHorarioMedicoService
{
    Task<HorarioMedicoDto> AgregarHorario(CreateHorarioMedicoDto horario);
    Task<HorarioMedicoDto> ActualizarHorario(int id, CreateHorarioMedicoDto horario);
    Task EliminarHorario(int id);  
    Task<HorarioMedicoDto> ObtenerHorarioPorId(int id);
    Task<IEnumerable<HorarioMedicoDto>> ObtenerHorariosPorMedicoId(int id);
    Task<IEnumerable<HorarioMedicoDto>> ListarHorarios();
}
