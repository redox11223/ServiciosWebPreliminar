using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface IEspecialidadService
{
    void AgregarEspecialidad(CreateEspecialidadDto especialidad);
    List<EspecialidadDto> ObtenerEspecialidades();
    void EliminarEspecialidad(int id);
    void ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad);
    
}
