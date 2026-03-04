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
        throw new NotImplementedException();
    }

    public Medico AgregarMedico(CreateMedicoDto medico)
    {
        throw new NotImplementedException();
    }

    public void EliminarMedico(int id)
    {
        throw new NotImplementedException();
    }

    public List<Medico> ObtenerMedicos()
    {
        throw new NotImplementedException();
    }

    public Medico ObtenerMedico(int id)
    {
        throw new NotImplementedException();
    }
}
