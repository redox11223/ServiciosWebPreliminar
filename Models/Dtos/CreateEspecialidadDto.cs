namespace preliminarServicios.Models.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateEspecialidadDto(    
    [Required]
    [Length(3, 100)]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios")]
    string Nombre
);
