using Microsoft.EntityFrameworkCore;
using System.Reflection;
using VirtualIT.Domain.Klanten;
using VirtualIT.Domain.Beheerders;
using VirtualIT.Domain.VirtualMachines;
using VirtualIT.Domain.Servers;
using VirtualIT.Domain.Templates;

namespace VirtualIT.Persistence;

public class VirtualITDbContext : DbContext
{
    public DbSet<Klant> Klanten => Set<Klant>();
    public DbSet<Beheerder> Beheerders => Set<Beheerder>();
    public DbSet<VirtualMachine> VirtualMachines => Set<VirtualMachine>();
    public DbSet<Server> Servers => Set<Server>();
    public DbSet<Template> Templates => Set<Template>();

    public VirtualITDbContext(DbContextOptions<VirtualITDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.EnableDetailedErrors();
        optionsBuilder.EnableSensitiveDataLogging();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder) {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}