using MainService.DAL.Features.Translations.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MainService.DAL.Configurations;

public class TranslationConfiguration : IEntityTypeConfiguration<Translation>
{
    public void Configure(EntityTypeBuilder<Translation> builder)
    {
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
    }
}