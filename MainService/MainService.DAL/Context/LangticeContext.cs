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
        
        //Seed data
        modelBuilder.Entity<Language>().HasData(
            new Language { Id = Guid.Parse("8dc05007-6ed9-406a-9eeb-fbbf748283e2"), Name = "English" },
            new Language { Id = Guid.Parse("8e5a2463-e8d1-427a-bd84-9386e073999f"), Name = "German" },
            new Language { Id = Guid.Parse("419ce969-51ab-41c9-9d2f-ae0f007d3b2d"), Name = "Russian" },
            new Language { Id = Guid.Parse("5715abc0-de4e-4a5c-bd9c-4edcbade3e09"), Name = "French" }
        );
        
        modelBuilder.Entity<User>().HasData(
            new User
            {
                Id = Guid.Parse("a1b2c3d4-e5f6-4a7b-8c9d-1234567890ab"),
                Username = "testuser",
                Email = "testuser@example.com",
                PasswordHash = "AQAAAAIAAYagAAAAEFakeHashedPassword1234567890", // placeholder hash
                AvatarUrl = null,
                Status = true
            }
        );
    }
}
