using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface ICitaService
{
    CitaDto AgregarCita(CreateCitaDto cita);
    List<CitaDto> ObtenerCitas();
    CitaDto ObtenerCita(int id);
    void EliminarCita(int id);
    CitaDto ActualizarCita(int id, CreateCitaDto cita);   
}
