using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using preliminarServicios.Models.Enums;

namespace preliminarServicios.Services;

public class CitaService : ICitaService
{
    private readonly List<Cita> _citas = [];
    private int _nextId = 1;

    private readonly IPacienteService _pacienteService;
    private readonly IMedicoService _medicoService;
    private readonly IHorarioMedicoService _horarioMedicoService;

    public CitaService(IPacienteService pacienteService, IMedicoService medicoService, IHorarioMedicoService horarioMedicoService)
    {
        _pacienteService = pacienteService;
        _medicoService = medicoService;
        _horarioMedicoService = horarioMedicoService;
    }

    public CitaDto ActualizarCita(int id, CreateCitaDto cita)
    {
        var citaExistente = _citas.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        if (citaExistente.Estado == CitaEstado.Cancelada || citaExistente.Estado == CitaEstado.Completada)
        {
            throw new InvalidOperationException("No se puede actualizar una cita cancelada o completada.");
        }
        var paciente = _pacienteService.ObtenerPaciente(cita.PacienteId);
        var medico = _medicoService.ObtenerMedico(cita.MedicoId);
        citaExistente.PacienteId = cita.PacienteId;
        citaExistente.MedicoId = cita.MedicoId;
        citaExistente.Costo = cita.Costo;
        citaExistente.Motivo = cita.Motivo;
        citaExistente.FechaInicio = cita.FechaInicio;
        citaExistente.FechaFin = cita.FechaInicio.AddMinutes(medico.DuracionCita);
        citaExistente.Observaciones = cita.Observaciones;
        citaExistente.Estado = CitaEstado.Confirmada;

        return MapearDto(citaExistente, paciente, medico);
    }

    public CitaDto AgregarCita(CreateCitaDto cita)
    {
        var paciente = _pacienteService.ObtenerPaciente(cita.PacienteId);
        var medico = _medicoService.ObtenerMedico(cita.MedicoId);
        var horarios = _horarioMedicoService.ObtenerHorarioPorMedicoId(cita.MedicoId);
        
        var fechaFinal = cita.FechaInicio.AddMinutes(medico.DuracionCita);
        
        bool conflicto = _citas.Any(c => c.MedicoId == cita.MedicoId && c.Estado != CitaEstado.Cancelada 
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
            Id = _nextId++,
            PacienteId = cita.PacienteId,
            MedicoId = cita.MedicoId,
            Costo = cita.Costo,
            Motivo = cita.Motivo,
            FechaInicio = cita.FechaInicio,
            FechaFin = fechaFinal,
            Observaciones = cita.Observaciones,
            Estado = CitaEstado.Confirmada
        };
        _citas.Add(newCita);
        return MapearDto(newCita, paciente, medico);
    }

    public void EliminarCita(int id)
    {
        var cita = _citas.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        _citas.Remove(cita);
    }

    public List<CitaDto> ObtenerCitas()
    {
        return _citas.Select(c =>
        {
            var paciente = _pacienteService.ObtenerPaciente(c.PacienteId);
            var medico = _medicoService.ObtenerMedico(c.MedicoId);
            return MapearDto(c, paciente, medico);
        }).ToList();
    }

    public CitaDto ObtenerCita(int id)
    {
        var cita = _citas.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        var paciente = _pacienteService.ObtenerPaciente(cita.PacienteId);
        var medico = _medicoService.ObtenerMedico(cita.MedicoId);

        return MapearDto(cita, paciente, medico);
    }

    private CitaDto MapearDto(Cita cita, PacienteDto paciente, MedicoResponseDto medico)
    {
        return new CitaDto(
            cita.Id,
            new PacienteResumenDto(
                cita.PacienteId,
                paciente.NombreCompleto,
                paciente.Dni
            ),
            new MedicoResumenDto(
                cita.MedicoId,
                $"{medico.Nombre} {medico.Apellido}",
                medico.EspecialidadNombre
            ),
            cita.FechaInicio,
            cita.FechaFin,
            cita.Motivo,
            cita.Estado.ToString()
        );
    }
}
           