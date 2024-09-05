using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace homework5;

public class ApplicaitonContext : DbContext
{
    public DbSet<Event> Events { get; set; }
    public DbSet<Guest> Guests { get; set; }
    public DbSet<EventGuest> EventGuests { get; set; }

    private IConfigurationRoot config = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json").Build();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<EventGuest>()
            .HasKey(eg => new { eg.EventId, eg.GuestId });

        modelBuilder.Entity<EventGuest>().Property(e => e.EventId).IsRequired();
        modelBuilder.Entity<EventGuest>().Property(e => e.GuestId).IsRequired();

        modelBuilder.Entity<EventGuest>()
            .HasOne(eg => eg.Event)
            .WithMany(e => e.EventGuests)
            .HasForeignKey(e => e.EventId);

        modelBuilder.Entity<EventGuest>()
            .HasOne(eg => eg.Guest)
            .WithMany(g => g.EventGuests)
            .HasForeignKey(eg => eg.GuestId);
        
        base.OnModelCreating(modelBuilder);
    }
}