using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public class PacienteService : IPacienteService
{
    private readonly List<Paciente> _pacientes = [];
    private int _nextId = 1;
    public Paciente ActualizarPaciente(int id, CreatePacienteDto paciente)
    {
        throw new NotImplementedException();
    }

    public Paciente AgregarPaciente(CreatePacienteDto paciente)
    {
        throw new NotImplementedException();
    }

    public void EliminarPaciente(int id)
    {
        throw new NotImplementedException();
    }

    public List<Paciente> ObtenerPacientes()
    {
        throw new NotImplementedException();
    }
}
