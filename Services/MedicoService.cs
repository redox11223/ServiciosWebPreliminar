using System;
using preliminarServicios.Data;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace preliminarServicios.Services;

public class MedicoService : IMedicoService
{
    private readonly ClinicaDbContext _context;
    private readonly IEspecialidadService _especialidadService;
    public MedicoService(ClinicaDbContext context, IEspecialidadService especialidadService)
    {
        _context = context;
        _especialidadService = especialidadService;
    }
    public async Task<MedicoResponseDto> ActualizarMedico(int id, CreateMedicoDto medico)
    {
        var existingMedico = await _context.Medicos.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");

        if (existingMedico.EspecialidadId != medico.EspecialidadId)
        {
            var especialidad = await _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);
            existingMedico.Especialidad = especialidad;
        }

         if (string.IsNullOrWhiteSpace(medico.NumeroLicencia))
        {
            throw new KeyNotFoundException("La especialidad especificada no existe");
        }

        var numeroLicencia = medico.NumeroLicencia.Trim();

        if (await _context.Medicos.AnyAsync(m => m.NumeroLicencia==numeroLicencia && m.Id != id))
        {
            throw new InvalidOperationException("Ya existe un médico con este número de licencia");
        }

        existingMedico.Nombre = medico.Nombre.Trim();
        existingMedico.Apellido = medico.Apellido.Trim();
        existingMedico.NumeroLicencia = numeroLicencia;
        existingMedico.Telefono = medico.Telefono?.Trim();
        existingMedico.EspecialidadId = medico.EspecialidadId;
        existingMedico.DuracionCita = medico.DuracionCita;
        existingMedico.FechaModificacion = DateTime.Now;
        await _context.SaveChangesAsync();

        return MapearDto(existingMedico);
    }

    public async Task<MedicoResponseDto> AgregarMedico(CreateMedicoDto medico)
    {
       
        var especialidad = await _especialidadService.ObtenerEspecialidad(medico.EspecialidadId);

        var numeroLicencia = medico.NumeroLicencia.Trim();

        if (await _context.Medicos.AnyAsync(m => m.NumeroLicencia==numeroLicencia))
        {
            throw new InvalidOperationException("Ya existe un médico con este número de licencia");
        }

        Medico newMedico = new()
        {
            Nombre = medico.Nombre.Trim(),
            Apellido = medico.Apellido.Trim(),
            NumeroLicencia = numeroLicencia,
            Telefono = medico.Telefono?.Trim(),
            EspecialidadId = medico.EspecialidadId,
            Especialidad = especialidad,
            DuracionCita = medico.DuracionCita
        };
        
        _context.Add(newMedico);
        await _context.SaveChangesAsync();
        
        return MapearDto(newMedico);
    }
    public async Task EliminarMedico(int id)
    {
        var medico = await _context.Medicos.FirstOrDefaultAsync(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");
        _context.Remove(medico);
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<MedicoResponseDto>> ObtenerMedicos()
    {
        return await _context.Medicos.Include(m => m.Especialidad).Select(m => new MedicoResponseDto(
            m.Id,
            m.Nombre,
            m.Apellido,
            m.NumeroLicencia,
            m.Telefono,
            m.EspecialidadId,
            m.Especialidad.Nombre,
            m.DuracionCita
        )).ToListAsync();
    }

    public async Task<MedicoResponseDto> ObtenerMedico(int id)
    {
        var medico = await _context.Medicos.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == id) 
            ?? throw new KeyNotFoundException("El médico no existe");
        
        return MapearDto(medico);
    }

    private static MedicoResponseDto MapearDto(Medico medico)
    {
        return new MedicoResponseDto(
            medico.Id,
            medico.Nombre,
            medico.Apellido,
            medico.NumeroLicencia,
            medico.Telefono,
            medico.EspecialidadId,
            medico.Especialidad.Nombre,
            medico.DuracionCita
        );
    }
}