using System;
using preliminarServicios.Data;
using preliminarServicios.Models.Dtos;
using preliminarServicios.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace preliminarServicios.Services;

public class PacienteService : IPacienteService
{
    private readonly ClinicaDbContext _context;
    public PacienteService(ClinicaDbContext context)
    {
        _context = context;
    }

    public async Task<PacienteDto> ActualizarPaciente(int id, CreatePacienteDto paciente)
    {
        var existingPaciente = await _context.Pacientes.FirstOrDefaultAsync(p => p.Id == id) ?? throw new KeyNotFoundException("Este paciente no existe");
        if (await _context.Pacientes.AnyAsync(p => p.Dni.ToLower() == paciente.Dni.ToLower().Trim() && p.Id != id))
        {
            throw new InvalidOperationException("Este paciente ya existe");
        }
        existingPaciente.Nombre = paciente.Nombre;
        existingPaciente.Apellido = paciente.Apellido;
        existingPaciente.Dni = paciente.Dni.Trim();
        existingPaciente.Email = paciente.Email.Trim();
        existingPaciente.Telefono = paciente.Telefono?.Trim();
        existingPaciente.FechaNacimiento = paciente.FechaNacimiento;
        existingPaciente.FechaModificacion = DateTime.Now;
        await _context.SaveChangesAsync();
        return MapearAPacienteDto(existingPaciente);
             
    }

    public async Task<PacienteDto> AgregarPaciente(CreatePacienteDto paciente)
    {
        if( await _context.Pacientes.AnyAsync(p => p.Dni.ToLower() == paciente.Dni.ToLower().Trim()))
        {
            throw new InvalidOperationException("Este paciente ya existe");
        }
        if( await _context.Pacientes.AnyAsync(p => p.Email.ToLower() == paciente.Email.ToLower().Trim()))
        {
            throw new InvalidOperationException("Este Email ya existe");
        }
        var newPaciente = new Paciente
        {
            Nombre = paciente.Nombre.Trim(),
            Apellido = paciente.Apellido.Trim(),
            Dni = paciente.Dni.Trim(),
            Email = paciente.Email.Trim(),
            Telefono = paciente.Telefono?.Trim(),
            FechaNacimiento = paciente.FechaNacimiento
        };
        _context.Pacientes.Add(newPaciente);
        await _context.SaveChangesAsync();
        return MapearAPacienteDto(newPaciente);
    }

    public async Task EliminarPaciente(int id)
    {
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(e=>e.Id==id) ?? throw new KeyNotFoundException("Este paciente no existe");
        _context.Pacientes.Remove(paciente);
        await _context.SaveChangesAsync();
    }

    public async Task<PacienteDto> ObtenerPaciente(int id)
    {
        var paciente = await _context.Pacientes.FirstOrDefaultAsync(e=>e.Id==id) ?? throw new KeyNotFoundException("Este paciente no existe");
        return MapearAPacienteDto(paciente);
    }

    public async Task<IEnumerable<PacienteDto>> ObtenerPacientes()
    {
        // POR QUÉ SE HACE ASÍ (PROYECCIÓN DIRECTA):
        // 1. EFICIENCIA (SQL): Al usar .Select() antes de materializar la lista, Entity Framework 
        //    genera un SQL que solo pide las columnas necesarias, evitando traer datos pesados (como fotos o auditoría).
        // 2. TRADUCCIÓN A SQL: No usamos métodos privados (como MapearAPacienteDto) dentro del Select 
        //    porque _context.Pacientes es IQueryable. El proveedor de SQL no sabe traducir métodos 
        //    propios de C# a comandos SQL y lanzaría una excepción en tiempo de ejecución.
        // 3. MATERIALIZACIÓN: .ToListAsync() ejecuta la consulta en la base de datos y trae los 
        //    resultados a la memoria del servidor de forma asíncrona, evitando bloquear el hilo principal.
        return await _context.Pacientes.Select(p => new PacienteDto(
            p.Id,
            $"{p.Nombre} {p.Apellido}",
            p.Dni,
            p.Email,
            p.Telefono,
            p.FechaNacimiento
        )).ToListAsync();
    }

    private static PacienteDto MapearAPacienteDto(Paciente paciente)
    {
        return new PacienteDto(
            paciente.Id,
            $"{paciente.Nombre} {paciente.Apellido}",
            paciente.Dni,
            paciente.Email,
            paciente.Telefono,
            paciente.FechaNacimiento
        );
    }
}
