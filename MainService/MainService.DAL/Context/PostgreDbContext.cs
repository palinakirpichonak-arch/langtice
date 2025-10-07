using System.Reflection;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Features.Languages.Models;
using MainService.DAL.Features.Translations.Models;
using MainService.DAL.Features.Users.Models;
using MainService.DAL.Features.Words.Models;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Context;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserWord> UserWords { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<Translation> Translations { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
