using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using Microsoft.EntityFrameworkCore;
using preliminarServicios.Data;

namespace preliminarServicios.Services;

public class HorarioMedicoService : IHorarioMedicoService
{
    private readonly ClinicaDbContext _context;

    public HorarioMedicoService( ClinicaDbContext context)
    {
        _context = context;
    }
    public async Task<HorarioMedicoDto> ActualizarHorario(int id, CreateHorarioMedicoDto horario)
    {
        var horarioExistente = await _context.Horarios.FirstOrDefaultAsync(h => h.Id == id) ?? throw new KeyNotFoundException($"No se encontró un horario con ID {id}");
        var medico = await _context.Medicos.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == horario.MedicoId) 
            ?? throw new KeyNotFoundException("El médico no existe");
        horarioExistente.MedicoId = horario.MedicoId;
        horarioExistente.DiaSemana = horario.DiaSemana;
        horarioExistente.HoraInicio = horario.HoraInicio;
        horarioExistente.HoraFin = horario.HoraFin;
        horarioExistente.FechaModificacion = DateTime.Now;
        await _context.SaveChangesAsync();
        return MapToDto(horarioExistente);
    }

    public async Task<HorarioMedicoDto> AgregarHorario(CreateHorarioMedicoDto horario)
    {
        var medico = await _context.Medicos.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == horario.MedicoId) 
            ?? throw new KeyNotFoundException("El médico no existe");
        var newHorario = new HorarioMedico
        {
            MedicoId = horario.MedicoId,
            DiaSemana = horario.DiaSemana, 
            HoraInicio = horario.HoraInicio,
            HoraFin = horario.HoraFin,
            Medico = medico
        };
        _context.Add(newHorario);
        await _context.SaveChangesAsync();
        return MapToDto(newHorario);
    }

    public async Task EliminarHorario(int id)
    {
        var horario = await _context.Horarios.FirstOrDefaultAsync(h => h.Id == id) ?? throw new KeyNotFoundException($"No se encontró un horario con ID {id}");
        horario.Activo = false;
        horario.FechaEliminacion = DateTime.Now;
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<HorarioMedicoDto>> ListarHorarios()
    {
        return await _context.Horarios.Include(h => h.Medico).ThenInclude(m => m.Especialidad).Select(h =>  new HorarioMedicoDto(
            h.Id,
            new MedicoResumenDto(h.Medico.Id, $"{h.Medico.Nombre} {h.Medico.Apellido}", h.Medico.Especialidad.Nombre),
            h.DiaSemana,
            h.HoraInicio,
            h.HoraFin
        )
        ).ToListAsync();
    }

    public async Task<IEnumerable<HorarioMedicoDto>> ObtenerHorariosPorMedicoId(int id)
    {
        return await _context.Horarios.Include(h => h.Medico).ThenInclude(m => m.Especialidad).Where(h => h.MedicoId == id).Select(h =>  new HorarioMedicoDto(
            h.Id,
            new MedicoResumenDto(h.Medico.Id, $"{h.Medico.Nombre} {h.Medico.Apellido}", h.Medico.Especialidad.Nombre),
            h.DiaSemana,
            h.HoraInicio,
            h.HoraFin
        )
        ).ToListAsync();
    }

    public async Task<HorarioMedicoDto> ObtenerHorarioPorId(int id)
    {
        var horario = await _context.Horarios.Include(h => h.Medico).ThenInclude(m => m.Especialidad).FirstOrDefaultAsync(h => h.Id == id) ?? throw new KeyNotFoundException($"No se encontró un horario con ID {id}");
        return MapToDto(horario);
    }
    
    private HorarioMedicoDto MapToDto(HorarioMedico horario)
    {
        return new HorarioMedicoDto(
            horario.Id,
            new MedicoResumenDto(horario.Medico.Id, $"{horario.Medico.Nombre} {horario.Medico.Apellido}", horario.Medico.Especialidad.Nombre),
            horario.DiaSemana,
            horario.HoraInicio,
            horario.HoraFin
        );
    }

}
