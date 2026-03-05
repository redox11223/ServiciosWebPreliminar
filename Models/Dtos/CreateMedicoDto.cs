namespace preliminarServicios.Models.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateMedicoDto(
    [Required] 
    [Length(3, 100)]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
    string Nombre,
    
    [Required] 
    [Length(3, 100)]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El apellido solo puede contener letras y espacios")]
    string Apellido,

    [Required] string NumeroLicencia,

    [Phone] string? Telefono,
    
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una especialidad válida")]
    int EspecialidadId    
);