using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using preliminarServicios.Models.Enums;

namespace preliminarServicios.Services;

public class CitaService : ICitaService
{   private readonly List<Cita> _citas = [];
    private int _nextId = 1;

    private readonly IPacienteService _pacienteService;
    private readonly IMedicoService _medicoService;
    private readonly IEspecialidadService _especialidadService;

    public CitaService(IPacienteService pacienteService, IMedicoService medicoService, IEspecialidadService especialidadService)
    {
        _pacienteService = pacienteService;
        _medicoService = medicoService;
        _especialidadService = especialidadService;
    }

    public CitaDto ActualizarCita(int id, CreateCitaDto cita)
    {
        var citaExistente = _citas.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        var paciente = _pacienteService.ObtenerPaciente(cita.PacienteId);
        var medico = _medicoService.ObtenerMedico(cita.MedicoId);
        if(cita.FechaInicio<DateTime.Now) throw new InvalidOperationException("La fecha de la cita no puede ser en el pasado");
        citaExistente.PacienteId = cita.PacienteId;
        citaExistente.MedicoId = cita.MedicoId;
        citaExistente.Costo = cita.Costo;
        citaExistente.Motivo = cita.Motivo;
        citaExistente.FechaInicio = cita.FechaInicio;
        citaExistente.FechaFin = cita.FechaInicio.AddMinutes(30); 
        citaExistente.Observaciones = cita.Observaciones;
        citaExistente.Estado = CitaEstado.Confirmada;

        return MapearDto(citaExistente, paciente, medico);
    }

    public CitaDto AgregarCita(CreateCitaDto cita)
    {
        var paciente=_pacienteService.ObtenerPaciente(cita.PacienteId);
        var medico=_medicoService.ObtenerMedico(cita.MedicoId);
        if(cita.FechaInicio<DateTime.Now) throw new InvalidOperationException("La fecha de la cita no puede ser en el pasado"); 

        var newCita=new Cita
        {
          Id=_nextId++,
          PacienteId=cita.PacienteId,
          MedicoId=cita.MedicoId,
          Costo=cita.Costo,
          Motivo=cita.Motivo,
          FechaInicio=cita.FechaInicio,
          FechaFin=cita.FechaInicio.AddMinutes(30),
          Observaciones=cita.Observaciones,
          Estado=CitaEstado.Confirmada  
        };
        _citas.Add(newCita);
        return MapearDto(newCita,paciente,medico); 
    }

    public void EliminarCita(int id)
    {
        throw new NotImplementedException();
    }

    public List<CitaDto> ObtenerCitas()
    {
        return _citas.Select(c => 
        {
        var paciente = _pacienteService.ObtenerPaciente(c.PacienteId);
        var medico = _medicoService.ObtenerMedico(c.MedicoId);
         return MapearDto(c,paciente,medico);
        }
        ).ToList();
    }

    public CitaDto ObtenerCita(int id)
    {
        var cita = _citas.FirstOrDefault(c => c.Id == id) ?? throw new KeyNotFoundException("Esta cita no existe");
        var paciente = _pacienteService.ObtenerPaciente(cita.PacienteId);
        var medico = _medicoService.ObtenerMedico(cita.MedicoId);

        return MapearDto(cita,paciente,medico);
    }

    private CitaDto MapearDto(Cita cita,PacienteDto paciente,MedicoResponseDto medico)
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
            cita.Motivo,
            cita.Estado.ToString()   
        );
    }
}
           