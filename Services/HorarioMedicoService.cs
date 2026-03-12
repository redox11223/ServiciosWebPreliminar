using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public class HorarioMedicoService : IHorarioMedicoService
{
    private readonly List<HorarioMedico> _horarios = [];
    private int _nextId = 1;

    private readonly IMedicoService _medicoService;

    public HorarioMedicoService(IMedicoService medicoService)
    {
        _medicoService = medicoService;
    }
    public HorarioMedicoDto ActualizarHorario(int id, CreateHorarioMedicoDto horario)
    {
        var horarioExistente = _horarios.Find(h => h.Id == id) ?? throw new KeyNotFoundException($"No se encontró un horario con ID {id}");
        var medico = _medicoService.ObtenerMedico(horario.MedicoId);
        horarioExistente.MedicoId = horario.MedicoId;
        horarioExistente.DiaSemana = horario.DiaSemana;
        horarioExistente.HoraInicio = horario.HoraInicio;
        horarioExistente.HoraFin = horario.HoraFin;
        horarioExistente.FechaModificacion = DateTime.Now;

        return MapToDto(horarioExistente, medico);
    }

    public HorarioMedicoDto AgregarHorario(CreateHorarioMedicoDto horario)
    {
        var medico = _medicoService.ObtenerMedico(horario.MedicoId);
        var newHorario = new HorarioMedico
        {
            Id = _nextId++,
            MedicoId = horario.MedicoId,
            DiaSemana = horario.DiaSemana,
            HoraInicio = horario.HoraInicio,
            HoraFin = horario.HoraFin
        };
        _horarios.Add(newHorario);
        return MapToDto(newHorario, medico);
    }

    public void EliminarHorario(int id)
    {
        var horario = _horarios.Find(h => h.Id == id) ?? throw new KeyNotFoundException($"No se encontró un horario con ID {id}");
        _horarios.Remove(horario);
    }

    public List<HorarioMedicoDto> ListarHorarios()
    {
       return _horarios.Select(h => {
            var medico = _medicoService.ObtenerMedico(h.MedicoId);
            return MapToDto(h, medico);
        }).ToList();
    }

    public List<HorarioMedicoDto> ObtenerHorarioPorMedicoId(int id)
    {
        var horario = _horarios.FindAll(h => h.MedicoId == id);
        
        return horario.Select(h => {
            var medico = _medicoService.ObtenerMedico(h.MedicoId);
            return MapToDto(h, medico);
        }).ToList();
    }

    public HorarioMedicoDto ObtenerHorarioPorId(int id)
    {
        var horario = _horarios.Find(h => h.Id == id) ?? throw new KeyNotFoundException($"No se encontró un horario con ID {id}");
        var medico = _medicoService.ObtenerMedico(horario.MedicoId);
        return MapToDto(horario, medico);
    }
    
    private HorarioMedicoDto MapToDto(HorarioMedico horario,MedicoResponseDto medico)
    {
        return new HorarioMedicoDto(
            horario.Id,
            new MedicoResumenDto(medico.Id, $"{medico.Nombre} {medico.Apellido}", medico.EspecialidadNombre),
            horario.DiaSemana,
            horario.HoraInicio,
            horario.HoraFin
        );
    }

}
