namespace preliminarServicios.Models.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreatePacienteDto(
    [Required] 
    [Length(3, 100)]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre,
    
    [Required]
    [Length(3, 100)]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Apellido,
    
    [Required]
    [RegularExpression(@"^(?!\s*$)\d{8}$", ErrorMessage = "El DNI debe tener exactamente 8 dígitos.")]
    string Dni,

    [Required]
    [EmailAddress(ErrorMessage = "El correo electrónico no es válido.")]
    string Email,
    
    [Phone]
    string? Telefono,
    
    [Required] DateOnly FechaNacimiento
);
