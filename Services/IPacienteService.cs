using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IPacienteService
{
    List<PacienteDto> ObtenerPacientes();
    PacienteDto AgregarPaciente(CreatePacienteDto paciente);
    PacienteDto ObtenerPaciente(int id);
    void EliminarPaciente(int id);
    PacienteDto ActualizarPaciente(int id, CreatePacienteDto paciente); 
}
