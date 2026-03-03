using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public class EspecialidadService : IEspecialidadService
{
    private readonly List<EspecialidadDto> _especialidades = [];
    private int _nextId = 1;
    public void ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad)
    {
        throw new NotImplementedException();
    }

    public void AgregarEspecialidad(CreateEspecialidadDto especialidad)
    {
        throw new NotImplementedException();
    }

    public void EliminarEspecialidad(int id)
    {
        throw new NotImplementedException();
    }

    public List<EspecialidadDto> ObtenerEspecialidades()
    {
        throw new NotImplementedException();
    }
}
