using MainService.DAL.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations.PostgreSql;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        
        builder.HasKey(t => t.Id);

        builder
            .HasIndex(c => new { c.BaseLanguageId, c.LearningLanguageId })
            .IsUnique();
        
        builder
            .HasOne(c => c.LearningLanguage)
            .WithMany()
            .HasForeignKey(c => c.LearningLanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(c => c.BaseLanguage)
            .WithMany()
            .HasForeignKey(c => c.BaseLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.Property(c => c.Status)
            .HasDefaultValue(true);
        
        var englishLangId = Guid.Parse("8dc05007-6ed9-406a-9eeb-fbbf748283e2");
        var germanLangId  = Guid.Parse("8e5a2463-e8d1-427a-bd84-9386e073999f");
        
        builder.HasData(
            new Course
            {
                Id = Guid.Parse("7ff9cff2-4cf1-45db-aa70-855bb69e507d"),
                BaseLanguageId = englishLangId,
                LearningLanguageId = germanLangId,
                Status = true
            }
        );
    }
}