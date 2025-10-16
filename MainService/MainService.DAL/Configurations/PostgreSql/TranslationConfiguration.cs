using MainService.DAL.Features.Translations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
        builder.HasKey(t => t.Id);
        
        builder
            .HasIndex(t => new { t.FromWordId, t.ToWordId, t.CourseId })
            .IsUnique();
        
        builder
            .HasOne(t => t.FromWord)
            .WithMany()
            .HasForeignKey(t => t.FromWordId)
            .OnDelete(DeleteBehavior.Restrict); 

        builder
            .HasOne(t => t.ToWord)
            .WithMany()
            .HasForeignKey(t => t.ToWordId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder
            .HasOne(t => t.Course)
            .WithMany()
            .HasForeignKey(t => t.CourseId)
            .IsRequired(false);
        
        var courseId = Guid.Parse("7ff9cff2-4cf1-45db-aa70-855bb69e507d");
        builder.HasData(
            // English to German translations
            new Translation
            {
                Id = Guid.Parse("b1b2c3d4-e5f6-4789-abc1-123456789001"),
                FromWordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789001"),
                ToWordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789006"), CourseId = courseId
            },
            new Translation
            {
                Id = Guid.Parse("b1b2c3d4-e5f6-4789-abc1-123456789002"),
                FromWordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789002"),
                ToWordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789007"), CourseId = courseId
            },
            new Translation
            {
                Id = Guid.Parse("b1b2c3d4-e5f6-4789-abc1-123456789003"),
                FromWordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789003"),
                ToWordId = Guid.Parse("a1b2c3d4-e5f6-4789-abc1-123456789008"), CourseId = courseId
            });
    }
}