using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface IHorarioMedicoService
{
    HorarioMedicoDto AgregarHorario(CreateHorarioMedicoDto horario);
    HorarioMedicoDto ActualizarHorario(int id, CreateHorarioMedicoDto horario);
    void EliminarHorario(int id);  
    HorarioMedicoDto ObtenerHorarioPorId(int id);
    List<HorarioMedicoDto> ObtenerHorarioPorMedicoId(int id);
    List<HorarioMedicoDto> ListarHorarios();
}
