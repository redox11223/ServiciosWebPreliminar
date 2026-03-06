using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IMedicoService
{
    MedicoResponseDto AgregarMedico(CreateMedicoDto medico);
    List<MedicoResponseDto> ObtenerMedicos();
    MedicoResponseDto ObtenerMedico(int id);
    void EliminarMedico(int id);
    MedicoResponseDto ActualizarMedico(int id, CreateMedicoDto medico);
}
