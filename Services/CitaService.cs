using System;
using preliminarServicios.Data;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using preliminarServicios.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace preliminarServicios.Services;

public class CitaService : ICitaService
{
    private readonly ClinicaDbContext _context;
    private readonly IHorarioMedicoService _horarioMedicoService;

    public CitaService(ClinicaDbContext context, IHorarioMedicoService horarioMedicoService)
    {
        _context = context;
        _horarioMedicoService = horarioMedicoService;
    }

    public async Task<CitaDto> ActualizarCita(int id, CreateCitaDto cita)
    {
        var citaExistente = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        if (citaExistente.Estado == CitaEstado.Cancelada || citaExistente.Estado == CitaEstado.Completada)
        {
            throw new InvalidOperationException("No se puede actualizar una cita cancelada o completada.");
        }
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(e=>e.Id==cita.PacienteId) ?? throw new KeyNotFoundException("Este paciente no existe");
        var medico = await _context.Medicos.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == cita.MedicoId) ?? throw new KeyNotFoundException("Este médico no existe");
        citaExistente.PacienteId = cita.PacienteId;
        citaExistente.MedicoId = cita.MedicoId;
        citaExistente.Costo = cita.Costo;
        citaExistente.Motivo = cita.Motivo;
        citaExistente.FechaInicio = cita.FechaInicio;
        citaExistente.FechaFin = cita.FechaInicio.AddMinutes(medico.DuracionCita);
        citaExistente.Observaciones = cita.Observaciones;
        citaExistente.Estado = CitaEstado.Confirmada;
        citaExistente.FechaModificacion = DateTime.Now;
        await _context.SaveChangesAsync();
        return MapearDto(citaExistente);
    }

    public async Task<CitaDto> AgregarCita(CreateCitaDto cita)
    {
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(e=>e.Id==cita.PacienteId) ?? throw new KeyNotFoundException("Este paciente no existe");
        var medico = await _context.Medicos.Include(m => m.Especialidad).FirstOrDefaultAsync(m => m.Id == cita.MedicoId) ?? throw new KeyNotFoundException("Este médico no existe");
        var horarios = await _horarioMedicoService.ObtenerHorariosPorMedicoId(cita.MedicoId);
        
        var fechaFinal = cita.FechaInicio.AddMinutes(medico.DuracionCita);
        
        bool conflicto = await _context.Citas.AnyAsync(c => c.MedicoId == cita.MedicoId && c.Estado != CitaEstado.Cancelada 
                                    && c.FechaInicio < fechaFinal && c.FechaFin > cita.FechaInicio);
        
        bool horarioValido = horarios.Any(h => h.DiaSemana == cita.FechaInicio.DayOfWeek &&
            h.HoraInicio <= cita.FechaInicio.TimeOfDay &&
            h.HoraFin >= fechaFinal.TimeOfDay);
        
        if (!horarioValido || conflicto)
        {
            throw new InvalidOperationException("El médico no está disponible en el horario solicitado.");
        }
        var newCita = new Cita
        {
            PacienteId = cita.PacienteId,
            MedicoId = cita.MedicoId,
            Costo = cita.Costo,
            Motivo = cita.Motivo,
            FechaInicio = cita.FechaInicio,
            FechaFin = fechaFinal,
            Observaciones = cita.Observaciones,
            Estado = CitaEstado.Confirmada,
            Paciente=paciente,
            Medico=medico
        };
        _context.Add(newCita);
        await _context.SaveChangesAsync();
        return MapearDto(newCita);
    }

    public async Task EliminarCita(int id)
    {
        var cita = await _context.Citas.FirstOrDefaultAsync(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        cita.Activo = false; // Soft delete
        cita.FechaEliminacion = DateTime.Now; // Registrar fecha de eliminación
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<CitaDto>> ObtenerCitas()
    {
        return await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico).ThenInclude(m => m.Especialidad).Select(c =>
       new CitaDto(
            c.Id,
            new PacienteResumenDto(
                c.PacienteId,
                $"{c.Paciente.Nombre} {c.Paciente.Apellido}", 
                c.Paciente.Dni
            ),
            new MedicoResumenDto(
                c.MedicoId,
                $"{c.Medico.Nombre} {c.Medico.Apellido}",
                c.Medico.Especialidad.Nombre
            ),
            c.FechaInicio,
            c.FechaFin,
            c.Motivo,
            c.Estado.ToString(),
            c.FechaCreacion,
            c.FechaModificacion

        )).ToListAsync();
    }

    public async Task<CitaDto> ObtenerCita(int id)
    {
        var cita = await _context.Citas.Include(c => c.Paciente).Include(c => c.Medico)
        .ThenInclude(m => m.Especialidad).FirstOrDefaultAsync(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        return MapearDto(cita);
    }

    private static CitaDto MapearDto(Cita cita)
    {
        return new CitaDto(
            cita.Id,
            new PacienteResumenDto(
                cita.PacienteId,
                $"{cita.Paciente.Nombre} {cita.Paciente.Apellido}", 
                cita.Paciente.Dni
            ),
            new MedicoResumenDto(
                cita.MedicoId,
                $"{cita.Medico.Nombre} {cita.Medico.Apellido}",
                cita.Medico.Especialidad.Nombre
            ),
            cita.FechaInicio,
            cita.FechaFin,
            cita.Motivo,
            cita.Estado.ToString(),
            cita.FechaCreacion,
            cita.FechaModificacion

        );
    }
}
           