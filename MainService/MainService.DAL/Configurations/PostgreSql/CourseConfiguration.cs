using MainService.DAL.Features.Courses;
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
    }
}