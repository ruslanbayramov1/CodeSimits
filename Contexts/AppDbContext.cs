using CodeSimits.Configurations;
using CodeSimits.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CodeSimits.Contexts;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> opt) : base(opt)
    {
        
    }

    public DbSet<Course> Courses { get; set; }
    public DbSet<Enrollment> Enrollments { get; set; }
    public DbSet<Grade> Grades { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<ClassTask> Tasks { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(typeof(AppUserConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(ClassTaskConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(CourseConfiguration).Assembly);
        builder.ApplyConfigurationsFromAssembly(typeof(MaterialConfiguration).Assembly);



        base.OnModelCreating(builder);
    }
}
    