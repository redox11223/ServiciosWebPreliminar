using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public class MedicoService : IMedicoService
{
    private readonly List<Medico> _medicos = [];
    private int _nextId = 1;
    private readonly IEspecialidadService _especialidadService;
    public MedicoService(IEspecialidadService especialidadService)
    {
        _especialidadService = especialidadService;
    }
    public Medico ActualizarMedico(int id, CreateMedicoDto medico)
    {
        var existingMedico = _medicos.FirstOrDefault(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");

        try
        {
            _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("La especialidad especificada no existe");
        }

        if (_medicos.Any(m => m.NumeroLicencia.Equals(medico.NumeroLicencia, StringComparison.OrdinalIgnoreCase) 
            && m.Id != id))
        {
            throw new InvalidOperationException("Ya existe un médico con este número de licencia");
        }

        existingMedico.Nombre = medico.Nombre;
        existingMedico.Apellido = medico.Apellido;
        existingMedico.NumeroLicencia = medico.NumeroLicencia;
        existingMedico.Telefono = medico.Telefono;
        existingMedico.EspecialidadId = medico.EspecialidadId;

        return new Medico()
        {
            Id = existingMedico.Id,
            Nombre = existingMedico.Nombre,
            Apellido = existingMedico.Apellido,
            NumeroLicencia = existingMedico.NumeroLicencia,
            Telefono = existingMedico.Telefono,
            EspecialidadId = existingMedico.EspecialidadId
        };
    }

    public Medico AgregarMedico(CreateMedicoDto medico)
    {
        try
        {
            _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);
        }
        catch (KeyNotFoundException)
        {
            throw new InvalidOperationException("La especialidad especificada no existe");
        }

        if (_medicos.Any(m => m.NumeroLicencia.Equals(medico.NumeroLicencia, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Ya existe un médico con este número de licencia");
        }

        Medico newMedico = new()
        {
            Id = _nextId++,
            Nombre = medico.Nombre,
            Apellido = medico.Apellido,
            NumeroLicencia = medico.NumeroLicencia,
            Telefono = medico.Telefono,
            EspecialidadId = medico.EspecialidadId
        };
        
        _medicos.Add(newMedico);
        return newMedico;
    }

    public void EliminarMedico(int id)
    {
        var medico = _medicos.FirstOrDefault(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");
        _medicos.Remove(medico);
    }

    public List<Medico> ObtenerMedicos()
    {
        return _medicos.ToList();
    }

    public Medico ObtenerMedico(int id)
    {
        var medico = _medicos.FirstOrDefault(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");
        
        return new Medico()
        {
            Id = medico.Id,
            Nombre = medico.Nombre,
            Apellido = medico.Apellido,
            NumeroLicencia = medico.NumeroLicencia,
            Telefono = medico.Telefono,
            EspecialidadId = medico.EspecialidadId
        };
    }
}