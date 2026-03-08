namespace preliminarServicios.Models.Dtos;

public record class CitaDto(
    int Id,
    PacienteResumenDto Paciente,
    MedicoResumenDto Medico,
    DateTime FechaInicio,
    DateTime FechaFin,
    string Motivo,
    string Estado
);
