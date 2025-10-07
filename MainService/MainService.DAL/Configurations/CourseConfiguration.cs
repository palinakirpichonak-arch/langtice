using MainService.DAL.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> modelBuilder)
    {
        //----Course----
        modelBuilder
            .HasOne(c => c.LearningLanguage)
            .WithMany()
            .HasForeignKey(c => c.LearningLanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(c => c.BaseLanguage)
            .WithMany()
            .HasForeignKey(c => c.BaseLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Property(c => c.Status)
            .HasDefaultValue(true);
    }
}