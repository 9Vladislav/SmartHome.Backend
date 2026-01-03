using Microsoft.EntityFrameworkCore;
using SmartHome.Core.Domain.Entities;

namespace SmartHome.Core.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Sensor> Sensors => Set<Sensor>();
    public DbSet<SecurityState> SecurityStates => Set<SecurityState>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Incident> Incidents => Set<Incident>();
    public DbSet<Notification> Notifications => Set<Notification>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
