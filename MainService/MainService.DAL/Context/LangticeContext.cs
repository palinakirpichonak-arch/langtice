using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MainService.DAL.Models;

public partial class LangticeTestContext : DbContext
{
    public LangticeTestContext()
    {
    }

    public LangticeTestContext(DbContextOptions<LangticeTestContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Language> Languages { get; set; }

    public virtual DbSet<Lesson> Lessons { get; set; }

    public virtual DbSet<LessonСontent> LessonContents { get; set; }

    public virtual DbSet<Mistake> Mistakes { get; set; }

    public virtual DbSet<Translation> Translations { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserCourse> UserCourses { get; set; }

    public virtual DbSet<UserInfo> UserInfos { get; set; }

    public virtual DbSet<Word> Words { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseNpgsql();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("pgcrypto");

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("course_pkey");

            entity.ToTable("course");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.BaseLanguageId).HasColumnName("base_language_id");
            entity.Property(e => e.LearningLanguageId).HasColumnName("learning_language_id");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");

            entity.HasOne(d => d.BaseLanguage).WithMany(p => p.CourseBaseLanguages)
                .HasForeignKey(d => d.BaseLanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_base_language_id_fkey");

            entity.HasOne(d => d.LearningLanguage).WithMany(p => p.CourseLearningLanguages)
                .HasForeignKey(d => d.LearningLanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("course_learning_language_id_fkey");
        });

        modelBuilder.Entity<Language>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("language_pkey");

            entity.ToTable("language");

            entity.HasIndex(e => e.Name, "language_name_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Lesson>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lesson_pkey");

            entity.ToTable("lesson");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OrderNum).HasColumnName("order_num");

            entity.HasOne(d => d.Course).WithMany(p => p.Lessons)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("lesson_course_id_fkey");
        });

        modelBuilder.Entity<LessonСontent>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("lessoncontent_pkey");

            entity.ToTable("lessoncontent");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.LessonId).HasColumnName("lesson_id");
            entity.Property(e => e.MongoId)
                .HasMaxLength(24)
                .HasColumnName("mongo_id");
            entity.Property(e => e.OrderNum).HasColumnName("order_num");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");

            entity.HasOne(d => d.Lesson).WithMany(p => p.Lessoncontents)
                .HasForeignKey(d => d.LessonId)
                .HasConstraintName("lessoncontent_lesson_id_fkey");
        });

        modelBuilder.Entity<Mistake>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("mistake_pkey");

            entity.ToTable("mistake");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.ExerciseId).HasColumnName("exercise_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WordId).HasColumnName("word_id");

            entity.HasOne(d => d.Exercise).WithMany(p => p.Mistakes)
                .HasForeignKey(d => d.ExerciseId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("mistake_exercise_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Mistakes)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("mistake_user_id_fkey");

            entity.HasOne(d => d.Word).WithMany(p => p.Mistakes)
                .HasForeignKey(d => d.WordId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("mistake_word_id_fkey");
        });

        modelBuilder.Entity<Translation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("translation_pkey");

            entity.ToTable("translation");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.TargetLanguageId).HasColumnName("target_language_id");
            entity.Property(e => e.TranslationText)
                .HasMaxLength(255)
                .HasColumnName("translation_text");
            entity.Property(e => e.WordId).HasColumnName("word_id");

            entity.HasOne(d => d.TargetLanguage).WithMany(p => p.Translations)
                .HasForeignKey(d => d.TargetLanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("translation_target_language_id_fkey");

            entity.HasOne(d => d.Word).WithMany(p => p.Translations)
                .HasForeignKey(d => d.WordId)
                .HasConstraintName("translation_word_id_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("User_pkey");

            entity.ToTable("User");

            entity.HasIndex(e => e.Email, "User_email_key").IsUnique();

            entity.HasIndex(e => e.Username, "User_username_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AvatarUrl).HasColumnName("avatar_url");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .HasColumnName("email");
            entity.Property(e => e.PasswordHash).HasColumnName("password_hash");
            entity.Property(e => e.Status)
                .HasDefaultValue(true)
                .HasColumnName("status");
            entity.Property(e => e.Username)
                .HasMaxLength(50)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UserCourse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("usercourse_pkey");

            entity.ToTable("usercourse");

            entity.HasIndex(e => new { e.UserId, e.CourseId }, "usercourse_user_id_course_id_key").IsUnique();

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.CourseId).HasColumnName("course_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Course).WithMany(p => p.Usercourses)
                .HasForeignKey(d => d.CourseId)
                .HasConstraintName("usercourse_course_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Usercourses)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("usercourse_user_id_fkey");
        });

        modelBuilder.Entity<UserInfo>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("userinfo_pkey");

            entity.ToTable("userinfo");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.MistakesMade)
                .HasDefaultValue(0)
                .HasColumnName("mistakes_made");
            entity.Property(e => e.Period)
                .HasMaxLength(50)
                .HasColumnName("period");
            entity.Property(e => e.StreakLength)
                .HasDefaultValue(0)
                .HasColumnName("streak_length");
            entity.Property(e => e.TestsFinished)
                .HasDefaultValue(0)
                .HasColumnName("tests_finished");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.WordsLearned)
                .HasDefaultValue(0)
                .HasColumnName("words_learned");

            entity.HasOne(d => d.User).WithMany(p => p.Userinfos)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("userinfo_user_id_fkey");
        });

        modelBuilder.Entity<Word>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("word_pkey");

            entity.ToTable("word");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("gen_random_uuid()")
                .HasColumnName("id");
            entity.Property(e => e.AddedAt)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("added_at");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.Text)
                .HasMaxLength(255)
                .HasColumnName("text");
            entity.Property(e => e.Type)
                .HasMaxLength(50)
                .HasColumnName("type");
            entity.Property(e => e.UserId).HasColumnName("user_id");

            entity.HasOne(d => d.Language).WithMany(p => p.Words)
                .HasForeignKey(d => d.LanguageId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("word_language_id_fkey");

            entity.HasOne(d => d.User).WithMany(p => p.Words)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("word_user_id_fkey");
        });
    }
}
