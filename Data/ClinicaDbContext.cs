using System;
using Microsoft.EntityFrameworkCore;
using preliminarServicios.Models.Entities;

namespace preliminarServicios.Data;

public class ClinicaDbContext(DbContextOptions<ClinicaDbContext> options) : DbContext(options)
{
    public DbSet<Paciente> Pacientes => Set<Paciente>();
    public DbSet<Medico> Medicos => Set<Medico>();
    public DbSet<Especialidad> Especialidades => Set<Especialidad>();
    public DbSet<HorarioMedico> Horarios => Set<HorarioMedico>();
    public DbSet<Cita> Citas => Set<Cita>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Cita>()
            .Property(c => c.Costo)        
            .HasPrecision(18, 2); 


        //Filtros globales para soft delete
        modelBuilder.Entity<Paciente>().HasQueryFilter(p => p.Activo);    
        modelBuilder.Entity<Medico>().HasQueryFilter(m => m.Activo);    
        modelBuilder.Entity<Cita>().HasQueryFilter(c => c.Activo);
        modelBuilder.Entity<HorarioMedico>().HasQueryFilter(h => h.Activo);    
        modelBuilder.Entity<Especialidad>().HasQueryFilter(e => e.Activo);
    }
}
