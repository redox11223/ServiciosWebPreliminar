using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public class EspecialidadService : IEspecialidadService
{
    private readonly List<Especialidad> _especialidades = [];
    private int _nextId = 1;
    public Especialidad ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad)
    {
       throw new NotImplementedException();
    }

    public Especialidad AgregarEspecialidad(CreateEspecialidadDto especialidad)
    {
        Especialidad newEspecialidad=new(){
            Id=_nextId++,
            Nombre=especialidad.Nombre
            };
        _especialidades.Add(newEspecialidad);
        return newEspecialidad;
    }

    public void EliminarEspecialidad(int id)
    {
        throw new NotImplementedException();
    }

    public List<Especialidad> ObtenerEspecialidades()
    {
        throw new NotImplementedException();
    }
}
