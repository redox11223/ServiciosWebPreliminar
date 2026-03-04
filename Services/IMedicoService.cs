using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface IMedicoService
{
    Medico AgregarMedico(CreateMedicoDto medico);
    List<Medico> ObtenerMedicos();
    void EliminarMedico(int id);
    Medico ActualizarMedico(int id, CreateMedicoDto medico);   
}
