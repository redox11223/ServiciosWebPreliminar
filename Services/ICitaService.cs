using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface ICitaService
{
    Cita AgregarCita(CreateCitaDto cita);
    List<CitaDto> ObtenerCitas();
    Cita ObtenerCita(int id);
    void EliminarCita(int id);
    Cita ActualizarCita(int id, CreateCitaDto cita);   
}
