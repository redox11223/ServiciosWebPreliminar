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

        if (!_especialidadService.ExisteEspecialidad(medico.EspecialidadId))
        {
            throw new KeyNotFoundException("La especialidad especificada no existe");
        }

        var especialidad = _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);

        var numeroLicencia = medico.NumeroLicencia.Trim();

        if (_medicos.Any(m => m.NumeroLicencia.Equals(numeroLicencia, StringComparison.OrdinalIgnoreCase) 
            && m.Id != id))
        {
            throw new InvalidOperationException("Ya existe un médico con este número de licencia");
        }

        existingMedico.Nombre = medico.Nombre.Trim();
        existingMedico.Apellido = medico.Apellido.Trim();
        existingMedico.NumeroLicencia = numeroLicencia;
        existingMedico.Telefono = medico.Telefono?.Trim();
        existingMedico.EspecialidadId = medico.EspecialidadId;

        return new MedicoResponseDto(
            existingMedico.Id,
            existingMedico.Nombre,
            existingMedico.Apellido,
            existingMedico.NumeroLicencia,
            existingMedico.Telefono,
            existingMedico.EspecialidadId,
            especialidad.Nombre
        );
    }

    public MedicoResponseDto AgregarMedico(CreateMedicoDto medico)
    {
        if (!_especialidadService.ExisteEspecialidad(medico.EspecialidadId))
        {
            throw new KeyNotFoundException("La especialidad especificada no existe");
        }

        var especialidad = _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);

        var numeroLicencia = medico.NumeroLicencia.Trim();

        if (_medicos.Any(m => m.NumeroLicencia.Equals(numeroLicencia, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Ya existe un médico con este número de licencia");
        }

        Medico newMedico = new()
        {
            Id = _nextId++,
            Nombre = medico.Nombre.Trim(),
            Apellido = medico.Apellido.Trim(),
            NumeroLicencia = numeroLicencia,
            Telefono = medico.Telefono?.Trim(),
            EspecialidadId = medico.EspecialidadId
        };
        
        _medicos.Add(newMedico);
        
        return new MedicoResponseDto(
            newMedico.Id,
            newMedico.Nombre,
            newMedico.Apellido,
            newMedico.NumeroLicencia,
            newMedico.Telefono,
            newMedico.EspecialidadId,
            especialidad.Nombre
        );
    }
    public void EliminarMedico(int id)
    {
        var medico = _medicos.FirstOrDefault(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");
        _medicos.Remove(medico);
    }

    public List<MedicoResponseDto> ObtenerMedicos()
    {
        return _medicos.Select(m => 
        {
            var especialidad = _especialidadService.ObtenerEspecialidad(m.EspecialidadId);
            return new MedicoResponseDto(
                m.Id,
                m.Nombre,
                m.Apellido,
                m.NumeroLicencia,
                m.Telefono,
                m.EspecialidadId,
                especialidad.Nombre
            );
        }).ToList();
    }

    public MedicoResponseDto ObtenerMedico(int id)
    {
        var medico = _medicos.FirstOrDefault(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");
        
        var especialidad = _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);
        
        return new MedicoResponseDto(
            medico.Id,
            medico.Nombre,
            medico.Apellido,
            medico.NumeroLicencia,
            medico.Telefono,
            medico.EspecialidadId,
            especialidad.Nombre
        );
    }
}