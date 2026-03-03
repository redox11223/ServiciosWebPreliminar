using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public class PacienteService : IPacienteService
{
    private readonly List<PacienteDto> _pacientes = [];
    private int _nextId = 1;
    public void ActualizarPaciente(int id, CreatePacienteDto paciente)
    {
        throw new NotImplementedException();
    }

    public void AgregarPaciente(CreatePacienteDto paciente)
    {
        throw new NotImplementedException();
    }

    public void EliminarPaciente(int id)
    {
        throw new NotImplementedException();
    }

    public List<PacienteDto> ObtenerPacientes()
    {
        throw new NotImplementedException();
    }
}
