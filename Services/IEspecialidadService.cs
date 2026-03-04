using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IEspecialidadService
{
    Especialidad AgregarEspecialidad(CreateEspecialidadDto especialidad);
    List<Especialidad> ObtenerEspecialidades();
    void EliminarEspecialidad(int id);
    Especialidad ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad);
    
}
