using System;

namespace preliminarServicios.Models.Entities;

public class EntidadBase
{
    public int Id { get; set; }
    
    //campo para soft delete,en cita no confundir con el estado de la cita, 
    //este campo es para eliminar logicamente un registro sin borrarlo fisicamente de la base de datos
    public bool Activo { get; set; } = true; 
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime? FechaModificacion { get; set; }
    public DateTime? FechaEliminacion { get; set; }
}
