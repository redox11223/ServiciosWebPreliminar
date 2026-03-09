using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;
//comentario2
public class CitaService : ICitaService
{   private readonly List<CitaDto> _citas = [];
    private int _nextId = 1;

    private readonly IPacienteService _pacienteService;
    private readonly IMedicoService _medicoService;

    public CitaService(IPacienteService pacienteService, IMedicoService medicoService)
    {
        _pacienteService = pacienteService;
        _medicoService = medicoService;
    }

    public Cita ActualizarCita(int id, CreateCitaDto cita)
    {
        throw new NotImplementedException();
    }

    public Cita AgregarCita(CreateCitaDto cita)
    {
        throw new NotImplementedException();
    }

    public void EliminarCita(int id)
    {
        throw new NotImplementedException();
    }

    public List<CitaDto> ObtenerCitas()
    {
        throw new NotImplementedException();
    }

    public Cita ObtenerCita(int id)
    {
        throw new NotImplementedException();
    }
}
