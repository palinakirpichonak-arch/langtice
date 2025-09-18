using Microsoft.EntityFrameworkCore;
using MainService.DAL.Models;

namespace MainService.DAL;

public class LangticeContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<UserInfo> UserInfos { get; set; } = null!;
    public DbSet<UserCourse> UserCourses { get; set; } = null!;
    public DbSet<Course> Courses { get; set; } = null!;
    public DbSet<Lesson> Lessons { get; set; } = null!;
    public DbSet<LessonСontent> LessonContents { get; set; } = null!;
    public DbSet<Language> Languages { get; set; } = null!;
    public DbSet<Word> Words { get; set; } = null!;
    public DbSet<Translation> Translations { get; set; } = null!;

    public LangticeContext(DbContextOptions<LangticeContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>()
            .HasOne(u => u.Userinfo)
            .WithOne(ui => ui.User)
            .HasForeignKey<UserInfo>(ui => ui.UserId);

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserСourses)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.Course)
            .WithMany(c => c.UserCourses)
            .HasForeignKey(uc => uc.CourseId);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.LearningLanguage)
            .WithMany(l => l.CoursesAsLearning)
            .HasForeignKey(c => c.LearningLanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Course>()
            .HasOne(c => c.BaseLanguage)
            .WithMany(l => l.CoursesAsBase)
            .HasForeignKey(c => c.BaseLanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId);

        modelBuilder.Entity<LessonСontent>()
            .HasOne(lc => lc.Lesson)
            .WithMany(l => l.LessonContents)
            .HasForeignKey(lc => lc.LessonId);

        modelBuilder.Entity<Word>()
            .HasOne(w => w.Language)
            .WithMany(l => l.Words)
            .HasForeignKey(w => w.LanguageId);

        modelBuilder.Entity<Word>()
            .HasOne(w => w.User)
            .WithMany(u => u.Words)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.SetNull);

        modelBuilder.Entity<Translation>()
            .HasOne(t => t.Word)
            .WithMany(w => w.Translations)
            .HasForeignKey(t => t.WordId);

        modelBuilder.Entity<Translation>()
            .HasOne(t => t.TargetLanguage)
            .WithMany(l => l.Translations)
            .HasForeignKey(t => t.TargetLanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        base.OnModelCreating(modelBuilder);
    }
}
