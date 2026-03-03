namespace preliminarServicios.Models.Dtos;

public record class PacienteDto(
    int Id,
    string Nombre,
    string Apellido,
    string Dni,
    string Email,
    string? Telefono,
    DateOnly FechaNacimiento
);
