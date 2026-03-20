using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface IPacienteService
{
    Task<IEnumerable<PacienteDto>> ObtenerPacientes();
    Task<PacienteDto> AgregarPaciente(CreatePacienteDto paciente);
    Task<PacienteDto> ObtenerPaciente(int id);
    Task EliminarPaciente(int id);
    Task<PacienteDto> ActualizarPaciente(int id, CreatePacienteDto paciente);
}
