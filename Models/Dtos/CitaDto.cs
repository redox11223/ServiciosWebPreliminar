namespace preliminarServicios.Models.Dtos;

public record class CitaDto(
    int Id,
    PacienteResumenDto Paciente,
    MedicoResumenDto Medico,
    DateTime FechaHora,
    string Motivo,
    string Estado
);
