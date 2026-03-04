using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IPacienteService
{
    List<Paciente> ObtenerPacientes();
    Paciente AgregarPaciente(CreatePacienteDto paciente);
    Paciente ObtenerPaciente(int id);
    void EliminarPaciente(int id);
    Paciente ActualizarPaciente(int id, CreatePacienteDto paciente); 
}
