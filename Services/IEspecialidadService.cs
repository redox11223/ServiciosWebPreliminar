using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IEspecialidadService
{
    Task<Especialidad> AgregarEspecialidad(CreateEspecialidadDto especialidad);
    Task<IEnumerable<Especialidad>> ObtenerEspecialidades();
    Task<Especialidad> ObtenerEspecialidad(int id);
    Task EliminarEspecialidad(int id);
    Task<Especialidad> ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad);
    Task<bool> ExisteEspecialidad(int id);
}