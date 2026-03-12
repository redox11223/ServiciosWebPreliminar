using System;

namespace preliminarServicios.Models.Entities;

public class EntidadBase
{
    public int Id { get; set; }
    public DateTime FechaCreacion { get; set; } = DateTime.Now;
    public DateTime? FechaModificacion { get; set; }
}
