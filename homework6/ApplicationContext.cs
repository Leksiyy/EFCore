using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace homework6;

public class ApplicationContext : DbContext
{
    public DbSet<Student> Students { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Instructor> Instructors { get; set; }
    
    private IConfigurationRoot _config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(_config.GetConnectionString("DefaultConnection"));
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Student)
            .WithMany(e => e.Enrollments)
            .HasForeignKey(e => e.StudentId);
        
        modelBuilder.Entity<Enrollment>()
            .HasOne(e => e.Course)
            .WithMany(e => e.Enrollments)
            .HasForeignKey(e => e.CourseId);
        
        modelBuilder.Entity<Course>()
            .HasMany(e => e.Instructors)
            .WithMany(e => e.Courses)
            .UsingEntity(e => e.ToTable("CourseEnrollments"));
        
        base.OnModelCreating(modelBuilder);
    }
}