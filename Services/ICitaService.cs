using System;
using preliminarServicios.Models.Dtos;

namespace preliminarServicios.Services;

public interface ICitaService
{
    void AgregarCita(CreateCitaDto cita);
    List<CitaDto> ObtenerCitas();
    void EliminarCita(int id);
    void ActualizarCita(int id, CreateCitaDto cita);   
}
