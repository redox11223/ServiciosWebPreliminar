using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public class MedicoService : IMedicoService
{
    private readonly List<MedicoDto> _medicos = [];
    private int _nextId = 1;
    private readonly IEspecialidadService _especialidadService;
    public MedicoService(IEspecialidadService especialidadService)
    {
        _especialidadService = especialidadService;
    }
    public void ActualizarMedico(int id, CreateMedicoDto medico)
    {
        throw new NotImplementedException();
    }

    public void AgregarMedico(CreateMedicoDto medico)
    {
        throw new NotImplementedException();
    }

    public void EliminarMedico(int id)
    {
        throw new NotImplementedException();
    }

    public List<MedicoDto> ObtenerMedicos()
    {
        throw new NotImplementedException();
    }
}
