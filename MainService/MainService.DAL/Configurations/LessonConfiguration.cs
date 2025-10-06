using MainService.DAL.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations;

public class LessonConfiguration :  IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> modelBuilder)
    {
        modelBuilder
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId);
    }
}