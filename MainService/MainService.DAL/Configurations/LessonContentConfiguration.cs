using MainService.DAL.Features.Courses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations;

public class LessonContentConfiguration : IEntityTypeConfiguration<LessonСontent>
{
    public void Configure(EntityTypeBuilder<LessonСontent> builder)
    {
        builder
            .HasOne(lc => lc.Lesson)
            .WithMany(l => l.LessonContents)
            .HasForeignKey(lc => lc.LessonId);
    }
}