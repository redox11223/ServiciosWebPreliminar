namespace preliminarServicios.Models.Dtos;

public record class MedicoDto(
    int Id,
    string Nombre,
    string Apellido,
    string Dni,
    string Email,
    string? Telefono,
    string Especialidad
);
