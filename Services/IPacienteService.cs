using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface IPacienteService
{
    List<PacienteDto> ObtenerPacientes();
    void AgregarPaciente(CreatePacienteDto paciente);
    void EliminarPaciente(int id);
    void ActualizarPaciente(int id, CreatePacienteDto paciente); 
}
