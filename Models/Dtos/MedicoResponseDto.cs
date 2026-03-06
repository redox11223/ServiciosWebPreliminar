namespace preliminarServicios.Models.Dtos;

public record class MedicoResponseDto(
    int Id,
    string Nombre,
    string Apellido,
    string NumeroLicencia,
    string? Telefono,
    int EspecialidadId,
    string EspecialidadNombre
);