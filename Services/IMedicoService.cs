using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface IMedicoService
{
    void AgregarMedico(CreateMedicoDto medico);
    List<MedicoDto> ObtenerMedicos();
    void EliminarMedico(int id);
    void ActualizarMedico(int id, CreateMedicoDto medico);   
}
