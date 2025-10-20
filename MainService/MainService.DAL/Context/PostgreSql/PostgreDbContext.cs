using System.Reflection;
using MainService.DAL.Features.Courses;
using MainService.DAL.Features.Languages;
using MainService.DAL.Features.Lessons;
using MainService.DAL.Features.Translations;
using MainService.DAL.Features.UserCourse;
using MainService.DAL.Features.Users;
using MainService.DAL.Features.UserTest;
using MainService.DAL.Features.UserWord;
using MainService.DAL.Features.Words;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Context.PostgreSql;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserWord> UserWords { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<UserTest> UserTests { get; set; }
    public DbSet<Translation> Translations { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
