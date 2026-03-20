using System;
using Microsoft.EntityFrameworkCore;
using preliminarServicios.Data;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Services;

public class EspecialidadService : IEspecialidadService
{
    private readonly ClinicaDbContext _context;
    public EspecialidadService(ClinicaDbContext context)    {
        _context = context;
    } 
    //todo crear un Dto de respuesta y agregar un campo del numero de medicos asociados a cada especialidad
    public async Task<Especialidad> ActualizarEspecialidad(int id, CreateEspecialidadDto especialidad)
    {
        var existingEspecialidad = await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Esta especialidad no existe");
        if (await _context.Especialidades.AnyAsync(e => e.Nombre.Equals(especialidad.Nombre,StringComparison.OrdinalIgnoreCase) && e.Id != id))
        {
            throw new InvalidOperationException("Esta especialidad ya existe");
        }
        existingEspecialidad.Nombre = especialidad.Nombre;
        existingEspecialidad.FechaModificacion = DateTime.Now;
        await _context.SaveChangesAsync();
        return new Especialidad()
        {
            Id = existingEspecialidad.Id,
            Nombre = existingEspecialidad.Nombre.Trim()
        };
    }

    public async Task<Especialidad> AgregarEspecialidad(CreateEspecialidadDto especialidad)
    {
        if (await _context.Especialidades.AnyAsync(e => e.Nombre.Equals(especialidad.Nombre, StringComparison.OrdinalIgnoreCase)))
        {
            throw new InvalidOperationException("Esta especialidad ya existe");
        }
        
        Especialidad newEspecialidad=new(){
            Nombre=especialidad.Nombre.Trim()
            };
        _context.Especialidades.Add(newEspecialidad);
        await _context.SaveChangesAsync();
        return newEspecialidad;
    }

    public async Task EliminarEspecialidad(int id)
    {
        var especialidad = await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Esta especialidad no existe");
        _context.Especialidades.Remove(especialidad);
        await _context.SaveChangesAsync();
    }

    public async Task<Especialidad> ObtenerEspecialidad(int id)
    {
        //retornamos una copia para evitar modificaciones externas a la lista interna
        var especialidad = await _context.Especialidades.FirstOrDefaultAsync(e => e.Id == id) ?? throw new KeyNotFoundException("Esta especialidad no existe");
        return new Especialidad()
        {
            Id = especialidad.Id,
            Nombre = especialidad.Nombre
        };
    }

    public async Task<IEnumerable<Especialidad>> ObtenerEspecialidades()
    {
        return await _context.Especialidades.ToListAsync();
    }

    public async Task<bool> ExisteEspecialidad(int id)
    {
        return await _context.Especialidades.AnyAsync(e => e.Id == id);
    }
}