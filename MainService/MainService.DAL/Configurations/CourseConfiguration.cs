using System.Globalization;
using MainService.DAL.Features.Courses.Models;
using MainService.DAL.Models;
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
            .WithMany(l => l.CoursesAsLearning)
            .HasForeignKey(c => c.LearningLanguageId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder
            .HasOne(c => c.BaseLanguage)
            .WithMany(l => l.CoursesAsBase)
            .HasForeignKey(c => c.BaseLanguageId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}