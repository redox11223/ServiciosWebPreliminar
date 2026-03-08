namespace preliminarServicios.Models.Dtos;
using System.ComponentModel.DataAnnotations;
public record class CreateEspecialidadDto(    
    [Required(ErrorMessage = "El nombre de la especialidad es obligatorio.")]
    [Length(3, 100, ErrorMessage = "El nombre de la especialidad debe tener entre 3 y 100 caracteres.")]
    [RegularExpression(@"^(?!\s*$)[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y no puede estar vacío o solo espacios")]
    string Nombre
);
