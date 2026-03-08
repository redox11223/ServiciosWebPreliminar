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
        var existingEspecialidad = _especialidades.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException("Esta especialidad no existe");
        if (_especialidades.Any(e => e.Nombre.Equals(especialidad.Nombre,StringComparison.OrdinalIgnoreCase) && e.Id != id))
        {
            throw new InvalidOperationException("Esta especialidad ya existe");
        }
        existingEspecialidad.Nombre = especialidad.Nombre;
        return new Especialidad()
        {
            Id = existingEspecialidad.Id,
            Nombre = existingEspecialidad.Nombre.Trim()
        };
    }

    public Especialidad AgregarEspecialidad(CreateEspecialidadDto especialidad)
    {
        if (_especialidades.Any(e => e.Nombre.Equals(especialidad.Nombre,StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Esta especialidad ya existe");
        }
        
        Especialidad newEspecialidad=new(){
            Id=_nextId++,
            Nombre=especialidad.Nombre.Trim()
            };
        _especialidades.Add(newEspecialidad);
        return newEspecialidad;
    }

    public void EliminarEspecialidad(int id)
    {
        var especialidad = _especialidades.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException("Esta especialidad no existe");
        _especialidades.Remove(especialidad);
    }

    public Especialidad ObtenerEspecialidad(int id)
    {
        //retornamos una copia para evitar modificaciones externas a la lista interna
        var especialidad = _especialidades.FirstOrDefault(e => e.Id == id) ?? throw new KeyNotFoundException("Esta especialidad no existe");
        return new Especialidad()
        {
            Id = especialidad.Id,
            Nombre = especialidad.Nombre
        };
    }

    public List<Especialidad> ObtenerEspecialidades()
    {
        return _especialidades.ToList();
    }

    public bool ExisteEspecialidad(int id)
    {
        return _especialidades.Any(e => e.Id == id);
    }
}