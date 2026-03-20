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
}
