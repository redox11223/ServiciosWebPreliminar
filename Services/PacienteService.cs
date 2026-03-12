using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public class PacienteService : IPacienteService
{
    private readonly List<Paciente> _pacientes = [];
    private int _nextId = 1;
    public PacienteDto ActualizarPaciente(int id, CreatePacienteDto paciente)
    {
        var existingPaciente = _pacientes.FirstOrDefault(p => p.Id == id) ?? throw new KeyNotFoundException("Este paciente no existe");
        if (_pacientes.Any(p => p.Dni.Equals(paciente.Dni,StringComparison.OrdinalIgnoreCase) && p.Id != id))
        {
            throw new InvalidOperationException("Este paciente ya existe");
        }
        existingPaciente.Nombre = paciente.Nombre;
        existingPaciente.Apellido = paciente.Apellido;
        existingPaciente.Dni = paciente.Dni;
        existingPaciente.Email = paciente.Email;
        existingPaciente.Telefono = paciente.Telefono;
        existingPaciente.FechaNacimiento = paciente.FechaNacimiento;
        existingPaciente.FechaModificacion = DateTime.Now;
        return MapearAPacienteDto(existingPaciente);
             
    }

    public PacienteDto AgregarPaciente(CreatePacienteDto paciente)
    {
        if( _pacientes.Any(p => p.Dni.Equals(paciente.Dni,StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Este paciente ya existe");
        }
        if( _pacientes.Any(p => p.Email.Equals(paciente.Email,StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Este Email ya existe");
        }
        var newPaciente = new Paciente
        {
            Id = _nextId++,
            Nombre = paciente.Nombre.Trim(),
            Apellido = paciente.Apellido.Trim(),
            Dni = paciente.Dni.Trim(),
            Email = paciente.Email.Trim(),
            Telefono = paciente.Telefono?.Trim(),
            FechaNacimiento = paciente.FechaNacimiento
        };
        _pacientes.Add(newPaciente);
        return MapearAPacienteDto(newPaciente);
    }

    public void EliminarPaciente(int id)
    {
        var paciente = _pacientes.FirstOrDefault(e=>e.Id==id) ?? throw new KeyNotFoundException("Este paciente ya existe");
        _pacientes.Remove(paciente);
    }

    public PacienteDto ObtenerPaciente(int id)
    {
        var paciente = _pacientes.FirstOrDefault(e=>e.Id==id) ?? throw new KeyNotFoundException("Este paciente no existe");
        return MapearAPacienteDto(paciente);
    }

    public List<PacienteDto> ObtenerPacientes()
    {
        return _pacientes.Select(MapearAPacienteDto).ToList();
    }

    private PacienteDto MapearAPacienteDto(Paciente paciente)
    {
        return new PacienteDto(
            paciente.Id,
            $"{paciente.Nombre} {paciente.Apellido}",
            paciente.Dni,
            paciente.Email,
            paciente.Telefono,
            paciente.FechaNacimiento
        );
    }
}
