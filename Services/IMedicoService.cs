using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IMedicoService
{
    Task<MedicoResponseDto> AgregarMedico(CreateMedicoDto medico);
    Task<IEnumerable<MedicoResponseDto>> ObtenerMedicos();
    Task<MedicoResponseDto> ObtenerMedico(int id);
    Task EliminarMedico(int id);
    Task<MedicoResponseDto> ActualizarMedico(int id, CreateMedicoDto medico);
}
