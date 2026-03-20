using System;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public interface ICitaService
{
    Task<CitaDto> AgregarCita(CreateCitaDto cita);
    Task<IEnumerable<CitaDto>> ObtenerCitas();
    Task<CitaDto> ObtenerCita(int id);
    Task EliminarCita(int id);
    Task<CitaDto> ActualizarCita(int id, CreateCitaDto cita);   
}
