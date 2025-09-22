using Microsoft.EntityFrameworkCore;
using MainService.DAL.Models;

namespace MainService.DAL;

public class LangticeContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Word> Words { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<Course> Courses { get; set; }
    public DbSet<Lesson> Lessons { get; set; }
    public DbSet<LessonСontent> LessonContents { get; set; }
    public DbSet<UserWord> UserWords { get; set; }
    public DbSet<UserCourse> UserCourses { get; set; }
    public DbSet<Translation> Translations { get; set; }

    public LangticeContext(DbContextOptions<LangticeContext> options) : base(options)
    {
        
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        //----UserWord----
        modelBuilder.Entity<UserWord>()
            .HasKey(uw => new { uw.UserId, uw.WordId });

        modelBuilder.Entity<UserWord>()
            .HasOne(uw => uw.User)
            .WithMany(u => u.UserWords)
            .HasForeignKey(uw => uw.UserId);

        modelBuilder.Entity<UserWord>()
            .HasOne(uw => uw.Word)
            .WithMany(w => w.UserWords)
            .HasForeignKey(uw => uw.WordId);
        
        //----UserCourse----
        modelBuilder.Entity<UserCourse>()
            .HasKey(uc => new { uc.UserId, uc.CourseId });

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.User)
            .WithMany(u => u.UserCourses)
            .HasForeignKey(uc => uc.UserId);

        modelBuilder.Entity<UserCourse>()
            .HasOne(uc => uc.Course)
            .WithMany(c => c.UserCourses)
            .HasForeignKey(uc => uc.CourseId);
        
        //----Translation----
        modelBuilder.Entity<Translation>()
            .HasOne(t => t.FromWord)
            .WithMany(w => w.TranslationsFrom)
            .HasForeignKey(t => t.FromWordId)
            .OnDelete(DeleteBehavior.Restrict); 

        modelBuilder.Entity<Translation>()
            .HasOne(t => t.ToWord)
            .WithMany(w => w.TranslationsTo)
            .HasForeignKey(t => t.ToWordId)
            .OnDelete(DeleteBehavior.Restrict);
        
        modelBuilder.Entity<Translation>()
            .HasOne(t => t.Course)
            .WithMany()
            .HasForeignKey(t => t.CourseId)
            .IsRequired(false);
        
        //----Word----
        modelBuilder.Entity<Word>()
            .HasOne(w => w.Language)
            .WithMany(l => l.Words)
            .HasForeignKey(w => w.LanguageId);
        
        //----Course----
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
        
        //----Lesson----
        modelBuilder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId);
        
        //----LessonContent----
        modelBuilder.Entity<LessonСontent>()
            .HasOne(lc => lc.Lesson)
            .WithMany(l => l.LessonContents)
            .HasForeignKey(lc => lc.LessonId);
    }
}
