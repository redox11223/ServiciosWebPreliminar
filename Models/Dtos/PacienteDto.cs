namespace preliminarServicios.Models.Dtos;

public record class PacienteDto
(int Id,string NombreCompleto,string Dni,string Email,string? Telefono,DateOnly FechaNacimiento);