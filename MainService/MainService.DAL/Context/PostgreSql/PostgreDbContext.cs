using System.Reflection;
using MainService.DAL.Models.CoursesModel;
using MainService.DAL.Models.LanguagesModel;
using MainService.DAL.Models.LessonsModel;
using MainService.DAL.Models.TranslationsModel;
using MainService.DAL.Models.UserCourseModel;
using MainService.DAL.Models.UserStreakModel;
using MainService.DAL.Models.UserTestModel;
using MainService.DAL.Models.UserWordModel;
using MainService.DAL.Models.WordsModel;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Context.PostgreSql;

public class PostgreDbContext(DbContextOptions<PostgreDbContext> options) : DbContext(options)
{
    public DbSet<Word> Words { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<UserWord> UserWords { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<UserTest> UserTests { get; set; }
    public DbSet<Translation> Translations { get; set; }
    public DbSet<UserStreak> UserStreaks {get; set;}
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
